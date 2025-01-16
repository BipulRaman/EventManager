"use client";
import React from 'react';
import { PageCard } from '../../../components/PageCard';
import { FormControl, FormControlLabel, FormLabel, Radio, RadioGroup, Typography } from '@mui/material';
import { IdentityVerifyImage } from './IdentityVerifyImage';
import { IdentityVerifyScan } from './IdentityVerifyScan';

export const IdentityVerify: React.FC = () => {
    const [toggleScanner, setToggleScanner] = React.useState<boolean>(true);
    const [verificationMode, setVerificationMode] = React.useState<string>("scan");


    // change of radio button to toggle between scan and image
    const handleChange = (event: React.ChangeEvent<HTMLInputElement>): void => {
        setVerificationMode(event.target.value);
        if (event.target.value === "scan") {
            setToggleScanner(true);
        }
        else {
            setToggleScanner(false);
        }
    };


    return (
        <>
            <PageCard>
                <Typography variant="h6" component="h2" gutterBottom>
                    Verify Alumni ID using QR Code
                </Typography>
                <FormControl>
                    <FormLabel id="radio">Verification mode:</FormLabel>
                    <RadioGroup
                        row
                        aria-labelledby="radio"
                        name="radio"
                        value={verificationMode}
                        onChange={handleChange}
                    >
                        <FormControlLabel value="scan" control={<Radio />} label="Scan QR" />
                        <FormControlLabel value="image" control={<Radio />} label="QR Screenshot" />
                    </RadioGroup>
                </FormControl>
            </PageCard>
            {
                toggleScanner ? (<IdentityVerifyScan />) : (<IdentityVerifyImage />)
            }
        </>
    );
}