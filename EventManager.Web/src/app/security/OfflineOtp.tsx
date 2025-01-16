"use client";
import React, { useEffect } from "react"
import { PageCard } from "../../components/PageCard"
import { TextField, Typography, Grid, IconButton } from "@mui/material"
import QRCode from "react-qr-code";
import { ApiResponse, CallStatus } from "../../types/ApiTypes";
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import { GetUserEmail } from "../../utils/TokenHelper";
import useGlobalState from "../../state/GlobalState"
import { AuthServices } from "@/services/ServicesIndex";

const TextFieldStyle: React.CSSProperties = {
    width: "100%"
}

export const OfflineOtp: React.FC = () => {
    const { otpSecretState, setOtpSecretState } = useGlobalState();
    const userEmail = GetUserEmail();

    const handleCopy = () => {
        navigator.clipboard.writeText(otpSecretState.data);
    }

    useEffect(() => {
        AuthServices.GetOtpSecret()
            .then((res: { data: ApiResponse<string> }) => {
                const apiResponse = res.data as ApiResponse<string>;
                setOtpSecretState({
                    data: apiResponse.result,
                    status: CallStatus.Success,
                    timestamp: new Date()
                });
            })
            .catch(() => {
                setOtpSecretState({
                    data: "",
                    status: CallStatus.Failure,
                    timestamp: new Date()
                });
            });
    }, []);

    return (
        <>
            <PageCard>
                <Typography variant='h6' gutterBottom>Setup offline OTP</Typography>
                <Typography variant='body2' component={'div'}>
                    <span>Follow below steps to setup offline OTP:</span>
                    <ol style={{ padding: "20px" }}>
                        <li>Install Google Authenticator: If you have not already, download and install the Google Authenticator app on your smartphone or tablet. It is available for both iOS and Android.</li>
                        <li>Click on the button below to setup offline OTP.</li>
                        <li>Scan the QR code using the Authenticator app.</li>
                        <li>Alternatively you can also add manually if scan does not work. Copy below secret key and create a new entry manually.</li>
                    </ol>
                    {
                        otpSecretState.status === CallStatus.Success &&
                        (
                            <Grid container spacing={1}>
                                <Grid item xs={10}>
                                    <span>
                                        <TextField
                                            disabled
                                            id="outlined-disabled"
                                            label="Secret"
                                            defaultValue={otpSecretState.data}
                                            style={TextFieldStyle}
                                        />
                                    </span>
                                </Grid>
                                <Grid item xs={1}>
                                    <IconButton aria-label="copy" size="large" onClick={handleCopy}>
                                        <ContentCopyIcon fontSize="inherit" />
                                    </IconButton>
                                </Grid>

                            </Grid>
                        )
                    }

                </Typography>
                <br />
                <div>
                    {
                        otpSecretState.status === CallStatus.Success &&
                        (
                            <div style={{ height: "auto", maxWidth: 64, width: "100%" }}>
                                <QRCode
                                    size={200}
                                    style={{ height: "auto", maxWidth: "400" }}
                                    value={`otpauth://totp/${userEmail}?secret=${otpSecretState.data}&issuer=JNVSAlumni`}
                                    viewBox={`0 0 200 200`}
                                />
                            </div>
                        )
                    }
                </div>

            </PageCard>
        </>
    )
}