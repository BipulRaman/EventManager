"use client";
import React, { useEffect, useRef } from 'react';
import { PageCard } from '../../../components/PageCard';
import { PublicProfileResult } from '../../../types/ProfileApiTypes';
import { ApiResponse, Validator } from '../../../types/ApiTypes';
import { IdentityDisplay } from '../IdentityDisplay';
import { Alert, Button, Typography } from '@mui/material';
import { BrowserMultiFormatReader } from '@zxing/library';
import { ProfileServices } from '@/services/ServicesIndex';

const alertStyle: React.CSSProperties = {
    width: "auto",
    marginTop: "10px",
};

export const IdentityVerifyScan: React.FC = () => {
    const [scannedProfile, setScannedProfile] = React.useState<PublicProfileResult>({} as PublicProfileResult);
    const [showScanner, setShowScanner] = React.useState<boolean>(true);
    const [errorMessage, setErrorMessage] = React.useState<string>("");

    const videoRef = useRef<HTMLVideoElement>(null);
    const codeReaderRef = useRef<BrowserMultiFormatReader | null>(null); // Persist codeReader instance

    const handleScan = (result: string | null) => {
        if (result && Validator.IsGuid(result)) {
            ProfileServices.GetProfileById(result)
                .then((response: { data: ApiResponse<PublicProfileResult> }) => {
                    const apiResult = response.data as ApiResponse<PublicProfileResult>;
                    setScannedProfile(apiResult.result);
                    setShowScanner(false);
                    setErrorMessage("");
                })
                .catch((error: Error) => {
                    console.log(error);
                });
        } else {
            setScannedProfile({} as PublicProfileResult);
        }
    };

    const handleReset = () => {
        console.log("Resetting scanner");
        setScannedProfile({} as PublicProfileResult);
        setShowScanner(true);
        setErrorMessage("");
    };

    useEffect(() => {
        if (!showScanner) return; // Skip initializing scanner if not needed

        const codeReader = new BrowserMultiFormatReader();
        codeReaderRef.current = codeReader; // Save the instance for resetting
        codeReader
            .listVideoInputDevices()
            .then((videoInputDevices) => {
                if (videoInputDevices.length > 0) {
                    const selectedDeviceId = videoInputDevices.find(device => device.label.toLowerCase().includes('back') || device.label.toLowerCase().includes('rear'))?.deviceId || videoInputDevices[0].deviceId;
                    codeReader.decodeFromVideoDevice(
                        selectedDeviceId,
                        videoRef.current,
                        (result, error) => {
                            if (result) {
                                handleScan(result.getText());
                                codeReader.reset(); // Reset scanner after successful scan
                            }
                            if (error) {
                                //handleError(error.message);
                            }
                        }
                    );
                } else {
                    console.error('No video input devices found');
                }
            })
            .catch((err) => console.error(err));

        return () => {
            codeReader.reset(); // Clean up scanner on component unmount
        };
    }, [showScanner]); // Reinitialize scanner when `showScanner` changes

    return (
        <>
            {showScanner && (
                <PageCard>
                    <Typography variant="h6" component="div" align="left">
                        Scan Alumni Badge QR Code
                    </Typography>
                    <video ref={videoRef} style={{ maxHeight: '300px' }} />
                    <div>
                        {errorMessage && (
                            <Alert style={alertStyle} severity="error">{errorMessage}</Alert>
                        )}
                    </div>
                </PageCard>
            )}
            {scannedProfile.id && (
                <PageCard>
                    <IdentityDisplay {...scannedProfile} />
                    <br />
                    <Button variant="contained" onClick={handleReset}>Reset</Button>
                </PageCard>
            )}
        </>
    );
};
