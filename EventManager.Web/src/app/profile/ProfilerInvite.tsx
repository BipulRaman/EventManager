"use client";
import * as React from 'react';
import { PageCard } from '../../components/PageCard';
import { Alert, Backdrop, Button, CircularProgress, Stack, TextField, Typography } from '@mui/material';
import { CallStatus, Validator } from '../../types/ApiTypes';
import { StateData } from '../../state/GlobalState';
import { ApiComponentStateManager } from '@/utils/ServiceStateHelper';
import { UserServices } from '@/services/ServicesIndex';
import { UserCreatePayload } from '@/types/UserApiTypes';

const submitButtonStyle: React.CSSProperties = {
    width: 90,
}

export const ProfileInvite = () => {
    const [formData, setFormData] = React.useState<UserCreatePayload>({} as UserCreatePayload);
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [userCreationState, setUserCreationState] = React.useState<StateData<boolean>>({} as StateData<boolean>);

    const validateForm = () => {
        const isValid = Validator.IsEmail(formData.email)
            && Validator.IsText(formData.name);
        setIsFormValid(isValid);
    }

    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        ApiComponentStateManager(UserServices.CreateUser(formData), setUserCreationState);
    }

    return (
        <PageCard>

            <form onSubmit={handleFormSubmit} onChange={validateForm}>
                <Stack
                    direction='column'
                    alignContent='center'
                    justifyContent='center'
                    spacing={2}
                    margin={2}
                    maxWidth={400}
                >
                    <Typography variant="h6" gutterBottom>
                        Register navodayan to this platform.
                    </Typography>
                    <TextField
                        label="Name"
                        onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                        error={formData.name !== '' && formData.name !== null && formData.name.length < 3}
                    />
                    <TextField
                        type='email'
                        label="Email"
                        onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                        error={formData.email !== '' && formData.email !== null && !Validator.IsEmail(formData.email)}
                    />
                    <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Submit</Button>
                    {
                        userCreationState.status === CallStatus.Success &&
                        (<Alert severity="success">Successfully submitted.</Alert>)
                    }
                    {
                        userCreationState.status === CallStatus.NotStarted &&
                        (<Alert severity="info">Ready!</Alert>)
                    }
                    {
                        userCreationState.status === CallStatus.Failure &&
                        (<Alert severity="error">Something went wrong. Try again.</Alert>)
                    }
                    <Backdrop
                        sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                        open={userCreationState.status === CallStatus.InProgress}
                    >
                        <CircularProgress color="inherit" />
                    </Backdrop>
                </Stack>
            </form>
        </PageCard>
    );
}