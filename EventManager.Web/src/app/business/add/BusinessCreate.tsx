"use client";
import React from 'react'
import { Alert, Backdrop, Button, CircularProgress, Divider, MenuItem, Stack, TextField, Typography } from '@mui/material'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemText from '@mui/material/ListItemText'
import ListItemAvatar from '@mui/material/ListItemAvatar'
import Avatar from '@mui/material/Avatar'
import { PageCard } from '../../../components/PageCard'
import BadgeIcon from '@mui/icons-material/Badge';
import { CallStatus } from '../../../types/ApiTypes'
import { StateData } from '@/state/GlobalState';
import { BusinessResult, CreateBusinessPayload } from '@/types/BusinessApiTypes';
import { BusinessServices } from '@/services/ServicesIndex';
import {BusinessCategories} from '@/constants/StaticLists';
import useGlobalState from '@/state/GlobalState';
import { ApiComponentStateManager, ApiGlobalStateManager } from '@/utils/ServiceStateHelper';

const submitButtonStyle: React.CSSProperties = {
    width: 150,
}

export const BusinessCreate: React.FunctionComponent = () => {
    const [formData, setFormData] = React.useState<CreateBusinessPayload>({} as CreateBusinessPayload);
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [isFormTouched, setIsFormTouched] = React.useState<boolean>(false);
    const [isSubmitted, setIsSubmitted] = React.useState<boolean>(false);
    const [businessCreationResult, setBusinessCreationResult] = React.useState<StateData<BusinessResult>>({} as StateData<BusinessResult>);
    const {setBusinessStateList} = useGlobalState();

    const validateName = (): boolean => {
        return !!formData.name && formData.name.length >= 3;
    }

    const validateCategory = (): boolean => {
        return !!formData.category && formData.category.length >= 3;
    }

    const validateDetails = (): boolean => {
        return !!formData.details && formData.details.length >= 3;
    }

    const validateAddress = (): boolean => {
        return !!formData.address && formData.address.length >= 3;
    }

    const validatePinCode = (): boolean => {
        return !!formData.pinCode && formData.pinCode.toString().length === 6;
    }

    const validatePhone = (): boolean => {
        const indianPhoneRegex = /^[6-9]\d{9}$/;
        return !!formData.phone && indianPhoneRegex.test(formData.phone);
    }

    const validateForm = () => {
        setIsFormValid(
            validateName() &&
            validateCategory() &&
            validateDetails() &&
            validatePhone() &&
            validateAddress() &&
            validatePinCode()           
        );
        return isFormValid;
    }

    // Handle Form Submit
    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsFormTouched(true);
        if (validateForm()) {
            setIsSubmitted(true);
            ApiComponentStateManager(BusinessServices.CreateBusiness(formData), setBusinessCreationResult).then(() => {
                ApiGlobalStateManager(BusinessServices.GetMyBusinessList(), setBusinessStateList);
            });
        }
    }

    return (
        <>
            <PageCard>
                {!isSubmitted ? (
                    <form onSubmit={handleFormSubmit} onChange={() => { setIsFormTouched(true); validateForm(); }}>
                        <List>
                            <ListItem>
                                <ListItemAvatar>
                                    <Avatar>
                                        <BadgeIcon />
                                    </Avatar>
                                </ListItemAvatar>
                                <ListItemText primary="Add your Business" />
                            </ListItem>
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <TextField
                                    label="Business Name"
                                    required
                                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                                    error={isFormTouched && !validateName()}
                                    value={formData.name}
                                />
                                <TextField
                                    select
                                    label="Category"
                                    required
                                    onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                                    error={isFormTouched && !validateCategory()}
                                    value={formData.category}
                                >
                                    {BusinessCategories.map((category) => (
                                        <MenuItem key={category} value={category}>
                                            {category}
                                        </MenuItem>
                                    ))}
                                </TextField>
                                <TextField
                                    label="Details / Specialisation"
                                    multiline
                                    rows={4}
                                    required
                                    onChange={(e) => setFormData({ ...formData, details: e.target.value })}
                                    error={isFormTouched && !validateDetails()}
                                    value={formData.details}
                                />
                                <TextField
                                    label="Phone"
                                    required
                                    onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
                                    error={isFormTouched && !validatePhone()}
                                    value={formData.phone}
                                />
                                <TextField
                                    label="Address"
                                    required
                                    onChange={(e) => setFormData({ ...formData, address: e.target.value })}
                                    error={isFormTouched && !validateAddress()}
                                    value={formData.address}
                                />
                                <TextField
                                    label="Pin Code"
                                    required
                                    onChange={(e) => setFormData({ ...formData, pinCode: Number(e.target.value) })}
                                    error={isFormTouched && !validatePinCode()}
                                    value={formData.pinCode}
                                />
                                <TextField
                                    label="GoogleMap Link"
                                    onChange={(e) => setFormData({ ...formData, mapLink: e.target.value })}
                                    value={formData.mapLink}
                                />                                
                                <TextField
                                    label="Email"                                    
                                    type="email"
                                    onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                                    value={formData.email}
                                />
                                <TextField
                                    label="Website"                                    
                                    onChange={(e) => setFormData({ ...formData, website: e.target.value })}
                                    value={formData.website}
                                />
                            </Stack>
                            <Divider />
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Submit</Button>
                                {
                                    businessCreationResult.status === CallStatus.Success && isSubmitted &&
                                    (<Alert severity="success">Successfully updated.</Alert>)
                                }
                                {
                                    !isSubmitted &&
                                    (<Alert severity="info">Please crosscheck your email before you submit.</Alert>)
                                }
                                {
                                    businessCreationResult.status === CallStatus.Failure && isSubmitted &&
                                    (<Alert severity="error">Something went wrong. Try again.</Alert>)
                                }
                                <Backdrop
                                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                                    open={businessCreationResult.status === CallStatus.InProgress}
                                >
                                    <CircularProgress color="inherit" />
                                </Backdrop>
                            </Stack>
                        </List>
                    </form>
                ) : (
                    <>
                        <Typography component="h4">Congratulations 🎉, your business <b>{businessCreationResult.data.name}</b> has been added!</Typography>
                    </>
                )}
            </PageCard>
        </>
    )
}
