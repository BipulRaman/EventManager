"use client";
import React, { useState, useContext } from 'react';
import { Alert, Backdrop, Button, CircularProgress, Stack, TextField, Typography } from '@mui/material';
import { PageCard } from '../../components/PageCard';
import LockOpenIcon from '@mui/icons-material/LockOpen';
import NumbersIcon from '@mui/icons-material/Numbers';
import { ImagePath } from '../../constants/StaticFiles';
import { useRouter } from 'next/navigation';
import { ApiResponse, Roles, Validator } from '../../types/ApiTypes';
import { SetAccessTokenData } from '../../utils/TokenHelper';
import { AuthContext, AuthData } from '../../utils/AuthContextHelper';
import { AuthTokenResult, CreateOtpPayload, CreateTokenPayload } from '../../types/AuthApiTypes';
import { Pathname } from '../../constants/Routes';
import { AuthServices } from '@/services/ServicesIndex';
import Image from 'next/image';

const alignSelfCenter: React.CSSProperties = {
  alignSelf: 'center'
};

const messageStyle: React.CSSProperties = {
  minHeight: '50px',
};

export interface LoginProps {
  pathname?: string;
}

export const Login: React.FC<LoginProps> = (props) => {
  const { setAuthData } = useContext(AuthContext);
  const [email, setEmail] = useState<string>("");
  const [otp, setOtp] = useState<string>("");
  const [showSuccess, setShowSuccess] = useState<boolean>(false);
  const [showError, setShowError] = useState<boolean>(false);
  const [successMessage, setSuccessMessage] = useState<string>('');
  const [errorMessage, setErrorMessage] = useState<string>('');
  const [isGetOtpEnabled, setIsGetOtpEnabled] = useState<boolean>(false);
  const [isLoginEnabled, setIsLoginEnabled] = useState<boolean>(false);
  const [isBackdropOpen, setIsBackdropOpen] = useState<boolean>(false);

  const redirectPathname = props.pathname || Pathname.Root;

  const router = useRouter();

  const hideAllMessages = () => {
    setShowError(false);
    setShowSuccess(false);
  };

  const handleOtpChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    hideAllMessages();
    setOtp(event.target.value);

    if (event.target.value.length === 6) {
      setIsLoginEnabled(true);
    } else {
      setIsLoginEnabled(false);
    }
  };

  const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    hideAllMessages();
    setEmail(event.target.value);

    if (Validator.IsEmail(event.target.value)) {
      setIsGetOtpEnabled(true);
    } else {
      setIsGetOtpEnabled(false);
      setIsLoginEnabled(false);
    }
  };

  const handleGetOtpClick = () => {
    hideAllMessages();
    setIsBackdropOpen(true);
    const payload: CreateOtpPayload = {
      email: email as string,
    };

    AuthServices.CreateOtp(payload)
      .then(() => {
        setIsBackdropOpen(false);
        setShowSuccess(true);
        setSuccessMessage('OTP sent successfully.');
      })
      .catch((res: { response: { status: number } }) => {
        setIsBackdropOpen(false);
        setShowError(true);
        if (res.response.status === 400) {
          setErrorMessage('Invalid email.');
        } else if (res.response.status === 403) {
          setErrorMessage('Access denied. Please contact support.');
        } else if (res.response.status === 404) {
          setErrorMessage('User not found. Please Register first.');
        } else {
          setErrorMessage('An unexpected error occurred. Please try again.');
        }
      });
  };

  const handleLoginClick = () => {
    hideAllMessages();
    setIsBackdropOpen(true);
    const payload: CreateTokenPayload = {
      email: email as string,
      otp: otp as string,
    };

    AuthServices.CreateToken(payload)
      .then((res: { data: ApiResponse<AuthTokenResult> }) => {
        const data = res.data;
        const authData: AuthData = {
          name: data.result.name,
          email: data.result.resource,
        };
        SetAccessTokenData(data.result);
        setAuthData(authData);
        setIsBackdropOpen(false);
        setShowSuccess(true);
        setSuccessMessage('Login successful.');
        if (data.result.roles.includes(Roles.InvitedUser)) {
          router.push('/profile');
        } else {
          router.push(redirectPathname);
        }
      })
      .catch((res: { response: { status: number } }) => {
        setIsBackdropOpen(false);
        setShowError(true);
        if (res.response.status === 403) {
          setErrorMessage('Access denied. Please contact support.');
        } else if (res.response.status === 400) {
          setErrorMessage('Invalid email or otp.');
        }
        else if (res.response.status === 404) {
          setErrorMessage('User not found. Please Register first.');
        }
        else {
          setErrorMessage('An unexpected error occurred. Please try again.');
        }
      });
  };

  return (
    <Stack
      direction='column'
      alignItems='center'
      justifyContent='center'
      height='80vh'
    >
      <PageCard>
        <form onSubmit={(e) => { e.preventDefault(); handleLoginClick(); }}>
          <Stack
            direction='column'
            alignContent='center'
            justifyContent='center'
            spacing={2}
            margin={2}
          >
            <Image
              src={ImagePath.logo}
              alt='Logo'
              width={100}
              height={100}
              style={alignSelfCenter}
            />
            <Typography variant='h6' style={alignSelfCenter}>Login to Portal</Typography>
            <TextField
              id='user-email'
              label='Email'
              variant='outlined'
              value={email}
              required
              onChange={handleEmailChange}
              error={email !== null && email !== "" && !Validator.IsEmail(email)}
            />
            <TextField
              id='otp'
              label='OTP'
              variant='outlined'
              value={otp}
              required
              onChange={handleOtpChange}
              error={otp !== null && otp !== "" && otp.length !== 6 && !Validator.IsNumber(otp)}
            />
            <Stack direction='row' justifyContent='center' spacing={2}>
              <Button
                variant='outlined'
                endIcon={<NumbersIcon />}
                disabled={!isGetOtpEnabled}
                onClick={handleGetOtpClick}
              >
                Get OTP
              </Button>
              <Button
                type='submit'
                variant='contained'
                endIcon={<LockOpenIcon />}
                disabled={!isLoginEnabled}
              >
                Login
              </Button>
            </Stack>
            {/* <Typography variant="body2" color="textSecondary">
              First time here? <Link href={Pathname.Register}>Register now</Link>
            </Typography> */}
          </Stack>
        </form>
      </PageCard>
      <div style={messageStyle}>
        {
          showError && (
            <Alert severity="error">{errorMessage}</Alert>
          )
        }
        {
          showSuccess && (
            <Alert severity="success">{successMessage}</Alert>
          )
        }
        {
          !showError && !showSuccess && (
            <Alert severity="info">Enter your credential to login.</Alert>
          )
        }
      </div>
      <Backdrop
        sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={isBackdropOpen}
        onClick={() => { setIsBackdropOpen(false); }}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    </Stack>
  );
};
