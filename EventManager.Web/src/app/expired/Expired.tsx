"use client";
import React, { useContext, useEffect } from 'react'
import { AuthContext } from '../../utils/AuthContextHelper'
import { Button, Stack, Typography } from '@mui/material'
import { ClearAccessTokenData } from '../../utils/TokenHelper'
import { useUpdateKeyContext } from '../../utils/KeyContextHelper'
import LockIcon from '@mui/icons-material/Lock';
import { useRouter } from 'next/navigation'

const buttonStyle: React.CSSProperties = {
    width: 100,
    alignSelf: 'center'
}

export const Expired: React.FC = () => {
    const { isAuthenticated, setIsAuthenticated } = useContext(AuthContext);
    const updateKey = useUpdateKeyContext();

    const router = useRouter();

    const handleResetAuth = () => {
        router.push('/login');
    }

    useEffect(() => {
        ClearAccessTokenData();             
        if(isAuthenticated){
            setIsAuthenticated(false);  
            updateKey();
        }        
    }, [])

    return (
        <Stack
            direction='column'
            flexWrap='wrap'
            alignContent='center'
            justifyContent='center'
            spacing={1}
            height='100%'
        >
            <Typography variant='h4'>Session expired!</Typography>
            <Typography textAlign="center">You need to login again.</Typography>
            <Button variant="contained" endIcon={<LockIcon />} onClick={handleResetAuth} style={buttonStyle}>
                Login
            </Button>
        </Stack>
    )
}
