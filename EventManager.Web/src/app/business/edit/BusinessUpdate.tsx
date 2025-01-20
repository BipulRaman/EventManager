"use client";
import React, { useEffect } from 'react'
import { Alert, Backdrop, Button, CircularProgress, Divider, MenuItem, Stack, TextField, Typography } from '@mui/material'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemText from '@mui/material/ListItemText'
import ListItemAvatar from '@mui/material/ListItemAvatar'
import Avatar from '@mui/material/Avatar'
import { PageCard } from '../../../components/PageCard'
import BadgeIcon from '@mui/icons-material/Badge';
import { ApiResponse, CallStatus } from '../../../types/ApiTypes'
import { StateData } from '@/state/GlobalState';
import { BusinessResult, UpdateBusinessPayload } from '@/types/BusinessApiTypes';
import { BusinessServices } from '@/services/ServicesIndex';
import { BusinessCategories } from '@/constants/StaticLists';
import { useRouter } from 'next/navigation';
import { Pathname } from '@/constants/Routes';
import Link from 'next/link';
import useStore from '@/state/GlobalState';
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';

const submitButtonStyle: React.CSSProperties = {
    width: 150,
}

export const BusinessUpdate: React.FunctionComponent = () => {
    const [formData, setFormData] = React.useState<UpdateBusinessPayload>({} as UpdateBusinessPayload);
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [isFormTouched, setIsFormTouched] = React.useState<boolean>(false);
    const [isSubmitted, setIsSubmitted] = React.useState<boolean>(false);
    const [businessUpdateResult, setBusinessUpdateResult] = React.useState<StateData<BusinessResult>>({} as StateData<BusinessResult>);
    const { businessStateList, setBusinessStateList } = useStore();
    const router = useRouter();

    useEffect(() => {
        const hash = window.location.hash;
        const budinessId = hash.split("#")[1];
        const business = Array.isArray(businessStateList.data) ? businessStateList.data.find(business => business.id === budinessId) : undefined;
        if (business) {
            setFormData(business);
        }
        else {
            BusinessServices.GetBusinessById(budinessId)
                .then(response => {
                    const apiResult = response.data as ApiResponse<BusinessResult>;
                    setFormData(apiResult.result as UpdateBusinessPayload);
                })
                .catch(() => {
                    router.push(Pathname.Business);
                });
        }
    }, []);

    const validateName = (): boolean => {
        return formData && !!formData.name && formData.name.length >= 3;
    }

    const validateCategory = (): boolean => {
        return formData && !!formData.category && formData.category.length >= 3;
    }

    const validateDetails = (): boolean => {
        return formData && !!formData.details && formData.details.length >= 3;
    }

    const validateAddress = (): boolean => {
        return formData && !!formData.address && formData.address.length >= 3;
    }

    const validatePinCode = (): boolean => {
        return formData && !!formData.pinCode && formData.pinCode.toString().length === 6;
    }

    const validatePhone = (): boolean => {
        const indianPhoneRegex = /^[6-9]\d{9}$/;
        return formData && !!formData.phone && indianPhoneRegex.test(formData.phone);
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
            ApiGlobalStateManager(BusinessServices.UpdateBusiness(formData), setBusinessUpdateResult).then(() => {
                const updatedBusinessList = businessStateList.data.map(business =>
                    business.id === formData.id ? { ...business, ...formData } : business
                );
                setBusinessStateList({ ...businessStateList, data: updatedBusinessList });
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
                                    value={formData.name || ''}
                                />
                                <TextField
                                    select
                                    label="Category"
                                    required
                                    onChange={(e) => setFormData({ ...formData, category: e.target.value })}
                                    error={isFormTouched && !validateCategory()}
                                    value={formData.category || ''}
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
                                    value={formData.details || ''}
                                />
                                 <TextField
                                    label="Phone"
                                    required
                                    onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
                                    error={isFormTouched && !validatePhone()}
                                    value={formData.phone || ''}
                                />
                                <TextField
                                    label="Address"
                                    required
                                    onChange={(e) => setFormData({ ...formData, address: e.target.value })}
                                    error={isFormTouched && !validateAddress()}
                                    value={formData.address || ''}
                                />
                                <TextField
                                    label="Pin Code"
                                    required
                                    onChange={(e) => setFormData({ ...formData, pinCode: Number(e.target.value) })}
                                    error={isFormTouched && !validatePinCode()}
                                    value={formData.pinCode || ''}
                                />
                                <TextField
                                    label="GoogleMap Link"                                    
                                    onChange={(e) => setFormData({ ...formData, mapLink: e.target.value })}
                                    value={formData.mapLink || ''}
                                />                               
                                <TextField
                                    label="Email"
                                    type="email"
                                    onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                                    value={formData.email || ''}
                                />
                                <TextField
                                    label="Website"
                                    onChange={(e) => setFormData({ ...formData, website: e.target.value })}
                                    value={formData.website || ''}
                                />
                            </Stack>
                            <Divider />
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Submit</Button>
                                {
                                    businessUpdateResult.status === CallStatus.Success && isSubmitted &&
                                    (<Alert severity="success">Successfully updated.</Alert>)
                                }
                                {
                                    !isSubmitted &&
                                    (<Alert severity="info">Please crosscheck your email before you submit.</Alert>)
                                }
                                {
                                    businessUpdateResult.status === CallStatus.Failure && isSubmitted &&
                                    (<Alert severity="error">Something went wrong. Try again.</Alert>)
                                }
                                <Backdrop
                                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                                    open={businessUpdateResult.status === CallStatus.InProgress}
                                >
                                    <CircularProgress color="inherit" />
                                </Backdrop>
                            </Stack>
                        </List>
                    </form>
                ) : (
                    <>
                        <Typography component="h4">Updated Successfully 🎉. <Link href={Pathname.Business}>Go back.</Link></Typography>
                    </>
                )}
            </PageCard>
        </>
    )
}
