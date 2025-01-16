"use client";
import React from "react";
import { Alert, Button, Divider } from "@mui/material";
import Paper from '@mui/material/Paper';
import InputBase from '@mui/material/InputBase';
import AddAPhotoIcon from '@mui/icons-material/AddAPhoto';
import { BrowserMultiFormatReader } from '@zxing/library';
import { ApiResponse, Validator } from "../../../types/ApiTypes";
import { PageCard } from "../../../components/PageCard";
import { PublicProfileResult } from "../../../types/ProfileApiTypes";
import { IdentityDisplay } from "../IdentityDisplay";
import { ProfileServices } from "@/services/ServicesIndex";

const alertStyle: React.CSSProperties = {
    width: "auto",
    marginTop: "10px",
}

export const IdentityVerifyImage = () => {
    const [displayName, setDisplayName] = React.useState<string>("");
    const [imageUrl, setImageUrl] = React.useState<string>("");
    const [errorMessage, setErrorMessage] = React.useState<string>("");
    const [showScanner, setShowScanner] = React.useState<boolean>(true);
    const [profileId, setProfileId] = React.useState<string>("");
    const [scannedProfile, setScannedProfile] = React.useState<PublicProfileResult>({} as PublicProfileResult);

    const decodeFromImageUrl = () => {
        if (imageUrl) {
            const codeReader = new BrowserMultiFormatReader();
            codeReader.decodeFromImageUrl(imageUrl)
                .then((result: { getText: () => string; }) => {
                    setErrorMessage("");
                    console.log(result);
                    const text = result.getText();
                    if (text && Validator.IsGuid(text)) {
                        console.log(text);
                        setProfileId(text);
                    }
                    else{
                        setErrorMessage("Invalid QR Code");
                    }
                })
                .catch(() => {
                    setErrorMessage("Unable to detect QR Code");
                });
        }     
    }

    const handleVerify = () => {
        decodeFromImageUrl();
        decodeFromImageUrl();
    }

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setErrorMessage("");
        const file = event.target.files?.[0];
        const fileBase64 = URL.createObjectURL(file as Blob);
        if (file) {
            setDisplayName(file.name);
            setImageUrl(fileBase64);
        }
    };

    const handleReset = () => {
        setErrorMessage("");
        setShowScanner(true);
        setScannedProfile({} as PublicProfileResult);
    }

    React.useEffect(() => {
        if(profileId && Validator.IsGuid(profileId)) {
            ProfileServices.GetProfileById(profileId)
                .then(response => {
                    const apiResult = response.data as ApiResponse<PublicProfileResult>;
                    setScannedProfile(apiResult.result);
                    setShowScanner(false);
                    setErrorMessage("");
                })
                .catch(error => {
                    console.log(error);
                });
        }
    }, [profileId]);

    return (
        <>
            {
                showScanner && (
                    <PageCard>
                        <Paper
                            component="form"
                            sx={{ p: '2px 4px', display: 'flex', alignItems: 'center', width: "100%", maxWidth: "300px" }}
                        >
                            <InputBase
                                sx={{ ml: 1, flex: 1 }}
                                placeholder="Select QR Image"
                                inputProps={{ 'aria-label': 'Select QR Image' }}
                                value={displayName}
                            />
                            <input
                                accept="image/*"
                                hidden
                                multiple={false}
                                id="qr-button-file"
                                type="file"
                                onChange={handleImageChange}
                            />
                            <label htmlFor="qr-button-file" >
                                <Button component="span" sx={{ p: '10px' }} >
                                    <AddAPhotoIcon />
                                </Button>
                            </label>
                            <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
                            <Button onClick={handleVerify} >Verify</Button>
                        </Paper>
                        <div>
                            {
                                errorMessage && (
                                    <Alert style={alertStyle} severity="error">{errorMessage}</Alert>
                                )
                            }
                        </div>
                    </PageCard>
                )
            }
            {
                scannedProfile.id && (
                    <PageCard>
                        <IdentityDisplay {...scannedProfile} />
                        <br /><br />
                        <Button variant="contained" onClick={() => { handleReset(); }}>Reset</Button>
                    </PageCard>
                )
            }
        </>
    );
};
