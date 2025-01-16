"use client";
import React, { useContext, useEffect } from 'react'
import { AuthContext } from '../../utils/AuthContextHelper'
import { Stack, Typography } from '@mui/material'
import { useRouter } from 'next/navigation'
import { ClearAccessTokenData } from '../../utils/TokenHelper'
import { useUpdateKeyContext } from '../../utils/KeyContextHelper'
import { ResetAllStores } from '@/state/GlobalState';

export const Logout: React.FC = () => {
  const router = useRouter();
  const { isAuthenticated, setIsAuthenticated } = useContext(AuthContext);
  const updateKey = useUpdateKeyContext();

  useEffect(() => {
    ClearAccessTokenData();
    ResetAllStores();
    if (isAuthenticated) {
      setIsAuthenticated(false);
      updateKey();
    }
    
    setTimeout(() => {
      router.push('/login');
    }, 2000);
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
      <Typography variant='h4'>Logout Successful</Typography>
      <Typography textAlign="center">Redirecting to Login page soon...</Typography>
    </Stack>
  )
}
