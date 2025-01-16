"use client";
import * as React from 'react';
import { PageCard } from '../../../components/PageCard';
import { Alert, Button, Divider } from "@mui/material";
import Paper from '@mui/material/Paper';
import InputBase from '@mui/material/InputBase';
import AddAPhotoIcon from '@mui/icons-material/AddAPhoto';   
import useGlobalState from '@/state/GlobalState';
import { ProfileServices } from '@/services/ServicesIndex';
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';

const alertStyle: React.CSSProperties = {
    width: "auto",
    marginTop: "10px",
}

const photoSubText: React.CSSProperties = {
    fontSize: "11px",
    color: "grey",
    marginLeft: "5px",
}

export const ProfilePhotoUpdate: React.FunctionComponent = () => {
    const { setProfileState } = useGlobalState();
    const [displayName, setDisplayName] = React.useState<string>("");
    const [imageFile, setImageFile] = React.useState<File | undefined>(undefined);
    const [message, setMessage] = React.useState<string>("");
    const [messageSeverity, setMessageSeverity] = React.useState<"error" | "success" | "info" | "warning" | undefined>(undefined);

    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setMessage("");
        const file = event.target.files?.[0];
        if (file) {
            if (!RegExp(/\.(jpg|jpeg|png|gif)$/).exec(file.name)) {
                setMessage("Please select valid image file");
                setMessageSeverity("error");
                return;
            }
            else if (file.size > 1000000) {
                setMessage("Please select image file less than 1 MB");
                setMessageSeverity("error");
                return;
            }
            setDisplayName(file.name);
            setImageFile(file);
        }
    };

    const handleUpload = () => {
        if (imageFile) {
            setMessage("");
            const data: FormData = new FormData();
            data.append("file", imageFile as Blob);
            
            ProfileServices.UpdateProfilePhoto(data)
                .then(() => {
                    setMessage("Profile photo updated successfully");
                    setMessageSeverity("success");
                    setImageFile(undefined);
                    setDisplayName("");
                    ApiGlobalStateManager(ProfileServices.GetProfile(), setProfileState);
                })
                .catch(() => {
                    setMessage("Profile photo update failed");
                    setMessageSeverity("error");
                });

        }
        else {
            setMessage("Please select an image file");
            setMessageSeverity("error");
        }
    };

    return (
        <PageCard>
            <Paper
                component="form"
                sx={{ padding: '5px 0px', display: 'flex', alignItems: 'center', width: "100%", maxWidth: "400px" }}
            >
                <InputBase
                    sx={{ ml: 1, flex: 1 }}
                    placeholder="Select profile photo"
                    inputProps={{ 'aria-label': 'Profile photo' }}
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
                    <Button component="span" sx={{ p: '10px', minWidth: '30px' }} >
                        <AddAPhotoIcon />
                    </Button>
                </label>
                <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
                <Button onClick={handleUpload} >Upload</Button>                
            </Paper>
            <span style={photoSubText}>(Photo should be square and less than 1 MB)</span>
            <div>
                {
                    message && messageSeverity && (
                        <Alert style={alertStyle} severity={messageSeverity}>{message}</Alert>
                    )
                }
            </div>
        </PageCard>
    );
};