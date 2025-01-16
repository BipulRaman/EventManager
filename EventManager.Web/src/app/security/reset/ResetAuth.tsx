"use client";
import React from "react"
import { PageCard } from "../../../components/PageCard"
import { Button, Typography, Alert } from "@mui/material"
import RestartAltIcon from '@mui/icons-material/RestartAlt';
import { CallStatus } from "../../../types/ApiTypes";
import { DateTimeReadable } from "../../../utils/CommonHelper";
import { useRouter } from 'next/navigation';
import { AuthServices } from "@/services/ServicesIndex";

export const ResetAuth: React.FC = () => {
    const [authReset, setAuthReset] = React.useState<CallStatus>(CallStatus.NotStarted);
    const [resetTimestamp, setResetTimestamp] = React.useState<Date | null>(null);

    const router = useRouter();

    const handleResetAuth = () => {
        AuthServices.ResetAuth()
            .then((res: { status: number }) => {
                if (res.status === 200) {
                    setAuthReset(CallStatus.Success);
                    setResetTimestamp(new Date());
                    router.push('/expired');
                }
            })
            .catch(() => {
                setAuthReset(CallStatus.Failure);
                setResetTimestamp(null);
            });
    }

    return (
        <>
            <PageCard>
                <Typography variant='h6' gutterBottom>Reset Authentication</Typography>
                <Typography variant='body2'>This option is to reset your authentication. After performing this action, you will be logged-out from all the devices. You will need to freshly login or freshly setup your offline OTP generation on Google Authenticator/ Microsoft Authenticator.</Typography>
                <br />
                <Button variant="contained" endIcon={<RestartAltIcon />} onClick={handleResetAuth}>
                    Reset
                </Button>
                <div><br /><Alert severity="warning">After RESET, You need to login again and setup authenticator again!</Alert></div>
                {
                    authReset === CallStatus.Success &&
                    (<div><br /><Alert severity="success">Successfully reset at {DateTimeReadable(resetTimestamp as Date)}</Alert></div>)
                }
                {
                    authReset === CallStatus.Failure &&
                    (<div><br /><Alert severity="error">Something went wrong. Try again.</Alert></div>)
                }
            </PageCard>
        </>
    )
}