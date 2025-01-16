"use client";
import React from 'react'
import { Alert, Backdrop, Button, Checkbox, Chip, CircularProgress, Divider, FormControl, FormControlLabel, FormGroup, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField, Typography } from '@mui/material'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemText from '@mui/material/ListItemText'
import ListItemAvatar from '@mui/material/ListItemAvatar'
import Avatar from '@mui/material/Avatar'
import { PageCard } from '../../components/PageCard'
import ContactMailIcon from '@mui/icons-material/ContactMail'
import WorkIcon from '@mui/icons-material/Work'
import SchoolIcon from '@mui/icons-material/School'
import BadgeIcon from '@mui/icons-material/Badge';
import { CreateProfilePayload, ProfileResult } from '../../types/ProfileApiTypes'
import { CallStatus, Gender, ProfileType, SchoolData, Validator } from '../../types/ApiTypes'
import { Schools } from '../../constants/StaticData'
import { StateData } from '@/state/GlobalState';
import { ApiComponentStateManager } from '@/utils/ServiceStateHelper';
import { ProfileServices } from '@/services/ServicesIndex';

const submitButtonStyle: React.CSSProperties = {
    width: 150,
}

export const ProfileCreate: React.FunctionComponent = () => {
    const hiddenText = '[Visible to you only]'
    const visibleText = '[Visible to all members]'
    const [formData, setFormData] = React.useState<CreateProfilePayload>({
        name: '',
        email: '',
        gender: Gender.Male,
        isContactInfoVisible: false,
        phone: '',
        location: '',
        latitude: 0,
        longitude: 0,
        profileType: ProfileType.Alumni,
        entryYear: 0,
        entryClass: 0,
        exitYear: 0,
        exitClass: 0,
        primarySchoolId: 0,
        secondarySchoolIds: [],
        isHigherEducationInfoVisible: false,
        isStudying: false,
        university: '',
        degree: '',
        isEmploymentInfoVisible: false,
        isWorking: false,
        organization: '',
        jobTitle: '',
    } as CreateProfilePayload);
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [isFormTouched, setIsFormTouched] = React.useState<boolean>(false);
    const [isSubmitted, setIsSubmitted] = React.useState<boolean>(false);
    const [selectedSchools, setSelectedSchools] = React.useState<SchoolData[]>([]);
    const [profile, setProfile] = React.useState<StateData<ProfileResult>>({} as StateData<ProfileResult>);
    const [isMobile, setIsMobile] = React.useState<boolean>();

    const handleSecondarySchoolIdsRemove = (schoolToDelete: SchoolData) => {
        const newSecondarySchoolIds = selectedSchools.filter((selectedSchools) => selectedSchools.UniqueId !== schoolToDelete.UniqueId);
        setSelectedSchools(newSecondarySchoolIds);
        const uniqueIds = newSecondarySchoolIds.map((school) => school.UniqueId);
        setFormData({ ...formData, secondarySchoolIds: uniqueIds });
        validateForm();
    };

    const handleSecondarySchoolIdsAdd = (schoolToAdd: SchoolData) => {
        if (!selectedSchools.find((selectedSchools) => selectedSchools.UniqueId === schoolToAdd.UniqueId)) {
            const newSecondarySchoolIds = [...selectedSchools, schoolToAdd];
            setSelectedSchools(newSecondarySchoolIds);
            const uniqueIds = newSecondarySchoolIds.map((school) => school.UniqueId);
            setFormData({ ...formData, secondarySchoolIds: uniqueIds });
        }
        validateForm();
    };

    // React.useEffect(() => {
    //     GetEmptyProfileManager(setProfile);
    // }, []);
    
    React.useEffect(() => {
        const handleResize = () => setIsMobile(window.innerWidth <= 768);
        window.addEventListener('resize', handleResize);
        handleResize();
        return () => window.removeEventListener('resize', handleResize);
    }, []);

    // Validate the Update Profile Payload
    const validateForm = (): boolean => {

        const isGeneralInfoValid: boolean = Validator.IsText(formData.name || '')
            && Validator.IsEmail(formData.email || '')
            && Validator.IsPhone(formData.phone || '')
            && Validator.IsText(formData.location || '')
            && Validator.IsText(formData.university || '')
            && Validator.IsText(formData.degree || '')

        const isAlumniInfoValid: boolean = isGeneralInfoValid
            && (formData.entryClass) > 0
            && (formData.entryYear) > 0
            && (formData.exitClass) > (formData.entryClass)
            && (formData.exitYear) > (formData.entryYear);

        const isStudentInfoValid: boolean = isGeneralInfoValid
            && (formData.entryClass) > 0
            && (formData.entryYear) > 0
            && (formData.exitClass) === 0
            && (formData.exitYear) === 0;

        const isStaffInfoValid: boolean = isGeneralInfoValid
            && Validator.IsText(formData.organization || '')
            && Validator.IsText(formData.jobTitle || '');

        if (formData.profileType === ProfileType.Student) {
            setIsFormValid(isStudentInfoValid);
            return isStudentInfoValid;
        }
        else if (formData.profileType === ProfileType.Staff) {
            setIsFormValid(isStaffInfoValid);
            return isStaffInfoValid;
        }
        else if (formData.profileType === ProfileType.Alumni) {
            setIsFormValid(isAlumniInfoValid);
            return isAlumniInfoValid;
        }
        else {
            setIsFormValid(isGeneralInfoValid);
            return isGeneralInfoValid;
        }
    }

    const handleProfileTypeChange = (e: SelectChangeEvent<ProfileType>) => {
        if (e.target.value === ProfileType.Student) {
            setFormData({ ...formData, profileType: e.target.value, isStudying: true, isWorking: false, exitClass: 0, exitYear: 0, university: '', degree: '', organization: '', jobTitle: '' });
        }
        else if (e.target.value === ProfileType.Staff) {
            setFormData({ ...formData, profileType: e.target.value, isStudying: false, isWorking: true, exitClass: 0, exitYear: 0, organization: 'Navodaya Vidyala Samiti' });
        }
        else {
            setFormData({ ...formData, profileType: e.target.value as ProfileType });
        }
    }

    // Handle Form Submit
    const handleFormSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setIsFormTouched(true);
        if (validateForm()) {            
            ApiComponentStateManager(ProfileServices.CreateProfile(formData), setProfile).finally(() => setIsSubmitted(true));
        }
    }

    return (
        <>
            <PageCard style={{ width: isMobile ? 'auto' : '700px', display: 'flex', justifyContent: 'center' }}>
                {!isSubmitted ? (
                    <form onSubmit={handleFormSubmit} onChange={() => { setIsFormTouched(true); validateForm(); }}>
                        <List>
                            <ListItem>
                                <ListItemAvatar>
                                    <Avatar>
                                        <BadgeIcon />
                                    </Avatar>
                                </ListItemAvatar>
                                <ListItemText primary="Navodaya Info" secondary={visibleText} />
                            </ListItem>
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <TextField
                                    label="Name"
                                    required
                                    onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                                    error={isFormTouched && !!formData.name && formData.name.length < 3}
                                    value={formData.name}
                                />
                                <FormControl>
                                    <InputLabel id="gender" >Gender</InputLabel>
                                    <Select
                                        labelId="gender"
                                        id="gender-select"
                                        value={formData.gender}
                                        label="Gender"
                                        required
                                        onChange={(e) => { setFormData({ ...formData, gender: e.target.value }); validateForm(); }}
                                    >
                                        <MenuItem selected disabled value="">Select</MenuItem>
                                        {Object.values(Gender).map((genderOption) => (
                                            <MenuItem key={genderOption} value={genderOption}>{genderOption}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                <FormControl>
                                    <InputLabel id="profileType" >Profile Type</InputLabel>
                                    <Select
                                        labelId="profileType"
                                        id="profileType-select"
                                        value={formData.profileType}
                                        label="Profile type"
                                        required
                                        onChange={(e) => { handleProfileTypeChange(e); validateForm(); }}
                                    >
                                        <MenuItem disabled value="">Select</MenuItem>
                                        <MenuItem key={ProfileType.Alumni} value={ProfileType.Alumni}>JNV Alumni</MenuItem>
                                        <MenuItem key={ProfileType.Student} value={ProfileType.Student}>Current JNV Student</MenuItem>
                                        <MenuItem key={ProfileType.ExStaff} value={ProfileType.ExStaff}>Former JNV Staff</MenuItem>
                                        <MenuItem key={ProfileType.Staff} value={ProfileType.Staff}>Current JNV Staff</MenuItem>
                                    </Select>
                                </FormControl>
                                <FormControl>
                                    <InputLabel id="entryJNV" >Entry JNV</InputLabel>
                                    <Select
                                        labelId="entryJNV"
                                        id="entryJNV-select"
                                        value={formData.primarySchoolId.toString()}
                                        defaultValue=""
                                        label="Entry JNV"
                                        required
                                        onChange={(e) => { setFormData({ ...formData, primarySchoolId: parseInt(e.target.value) }); validateForm(); }}
                                    >
                                        <MenuItem disabled value="0">
                                            Select
                                        </MenuItem>
                                        {Schools.map((school) => (
                                            <MenuItem key={school.UniqueId} value={school.UniqueId.toString()}>[{school.StatePrefix}] JNV {school.SchoolName}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                <FormControl>
                                    <InputLabel id="otherJNVs" >Other JNV</InputLabel>
                                    <Select
                                        labelId="otherJNVs"
                                        id="otherJNVs-select"
                                        label="Other JNVs"
                                        value="Add other JNVs attended"
                                        onChange={(e) => { handleSecondarySchoolIdsAdd(Schools.find((school) => school.UniqueId === parseInt(e.target.value)) as SchoolData); validateForm(); }}
                                    >
                                        <MenuItem disabled value="Add other JNVs attended">Add other JNVs attended</MenuItem>
                                        {Schools.filter((school) => !selectedSchools.find((selectedSchool) => selectedSchool.UniqueId === school.UniqueId)).map((school) => (
                                            <MenuItem key={school.UniqueId} value={school.UniqueId}>[{school.StatePrefix}] JNV {school.SchoolName}</MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                                <Stack
                                    sx={{
                                        display: 'flex',
                                        justifyContent: 'left',
                                        flexWrap: 'wrap',
                                    }}
                                >
                                    {selectedSchools.map((school) => {
                                        return (
                                            <ListItem key={school.UniqueId}>
                                                <Chip
                                                    label={`${school.StatePrefix} - ${school.SchoolName}`}
                                                    onDelete={() => handleSecondarySchoolIdsRemove(school)}
                                                />
                                            </ListItem>
                                        );
                                    })}
                                </Stack>
                                {
                                    (formData.profileType === ProfileType.Student || formData.profileType === ProfileType.Alumni) &&
                                    (
                                        <FormControl>
                                            <InputLabel id="entryClass">Entry class</InputLabel>
                                            <Select
                                                labelId="entryClass"
                                                id="entryClass-select"
                                                value={formData.entryClass.toString()}
                                                label="Entry class"
                                                required
                                                error={isFormTouched && (formData.profileType === ProfileType.Student ? false : formData.exitClass <= formData.entryClass)}
                                                onChange={(e) => { setFormData({ ...formData, entryClass: parseInt(e.target.value) }); validateForm(); }}
                                            >
                                                <MenuItem disabled value="0">Select</MenuItem>
                                                {
                                                    Array.from(Array(12 - 6 + 1).keys()).map((i) => i + 6).map((entryClassOption) => (
                                                        <MenuItem key={entryClassOption} value={entryClassOption}>{entryClassOption}</MenuItem>
                                                    ))
                                                }
                                            </Select>
                                        </FormControl>
                                    )
                                }
                                {
                                    formData.profileType === ProfileType.Alumni &&
                                    (
                                        <FormControl>
                                            <InputLabel id="exitClass">Exit class</InputLabel>
                                            <Select
                                                labelId="exitClass"
                                                id="exitClass-select"
                                                value={formData.exitClass.toString()}
                                                label="Exit class"
                                                required
                                                error={isFormTouched && formData.profileType === ProfileType.Alumni && formData.exitClass <= formData.entryClass}
                                                onChange={(e) => { setFormData({ ...formData, exitClass: parseInt(e.target.value) }); validateForm(); }}
                                            >
                                                <MenuItem selected disabled value="0">Select</MenuItem>
                                                {
                                                    Array.from(Array(12 - 6 + 1).keys()).map((i) => i + 6).map((exitClassOption) => (
                                                        <MenuItem key={exitClassOption} value={exitClassOption}>{exitClassOption}</MenuItem>
                                                    ))
                                                }
                                            </Select>
                                        </FormControl>
                                    )
                                }

                                <FormControl>
                                    <InputLabel id="entryYear">Entry year</InputLabel>
                                    <Select
                                        labelId="entryYear"
                                        id="entryYear-select"
                                        value={formData.entryYear?.toString()}
                                        label="Entry year"
                                        required
                                        error={isFormTouched && (formData.profileType === ProfileType.Student || formData.profileType === ProfileType.Staff ? false : formData.exitYear <= formData.entryYear)}
                                        onChange={(e) => { setFormData({ ...formData, entryYear: parseInt(e.target.value) }); validateForm(); }}
                                    >
                                        <MenuItem selected disabled value="0">Select</MenuItem>
                                        {
                                            Array.from(Array((new Date()).getFullYear() - 1985 + 1).keys()).map((i) => i + 1985).map((entryYearOption) => (
                                                <MenuItem key={entryYearOption} value={entryYearOption}>{entryYearOption}</MenuItem>
                                            ))
                                        }
                                    </Select>
                                </FormControl>

                                {
                                    (formData.profileType !== ProfileType.Student && formData.profileType !== ProfileType.Staff) &&
                                    (
                                        <FormControl>
                                            <InputLabel id="exitYear">Exit year</InputLabel>
                                            <Select
                                                labelId="exitYear"
                                                id="exitYear-select"
                                                value={formData.exitYear?.toString()}
                                                label="Exit year"
                                                error={isFormTouched && !!formData.entryYear && (formData.exitYear <= formData.entryYear)}
                                                required
                                                onChange={(e) => { setFormData({ ...formData, exitYear: parseInt(e.target.value) }); validateForm(); }}
                                            >
                                                <MenuItem selected disabled value="0">Select</MenuItem>
                                                {
                                                    Array.from(Array((new Date()).getFullYear() - 1985 + 1).keys()).map((i) => i + 1985).map((exitYearOption) => (
                                                        <MenuItem key={exitYearOption} value={exitYearOption}>{exitYearOption}</MenuItem>
                                                    ))
                                                }
                                            </Select>
                                        </FormControl>
                                    )
                                }
                            </Stack>
                            <Divider />
                            <ListItem>
                                <ListItemAvatar>
                                    <Avatar>
                                        <ContactMailIcon />
                                    </Avatar>
                                </ListItemAvatar>
                                <ListItemText
                                    primary='Contact Info'
                                    secondary={
                                        formData.isContactInfoVisible ? visibleText : hiddenText
                                    }
                                />
                            </ListItem>
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <FormControlLabel
                                    control={<Checkbox checked={formData.isContactInfoVisible}
                                        onChange={(e) => { setFormData({ ...formData, isContactInfoVisible: e.target.checked }); validateForm(); }} />}
                                    label="Visible to members" />
                                <TextField
                                    label="Email"
                                    onChange={(e) => { setFormData({ ...formData, email: e.target.value }); validateForm(); }}
                                    error={isFormTouched && !Validator.IsEmail(formData.email)}
                                    value={formData.email}
                                />
                                <TextField
                                    label="Phone"
                                    onChange={(e) => { setFormData({ ...formData, phone: e.target.value }); validateForm(); }}
                                    error={isFormTouched && !Validator.IsPhone(formData.phone)}
                                    value={formData.phone || ''}
                                />
                                <TextField
                                    label="Location"
                                    onChange={(e) => { setFormData({ ...formData, location: e.target.value }); validateForm(); }}
                                    error={isFormTouched && !Validator.IsText(formData.location)}
                                    value={formData.location || ''}
                                />
                            </Stack>
                            {
                                formData.profileType && formData.profileType !== ProfileType.Student && (
                                    <>
                                        <Divider />
                                        <ListItem>
                                            <ListItemAvatar>
                                                <Avatar>
                                                    <SchoolIcon />
                                                </Avatar>
                                            </ListItemAvatar>
                                            <ListItemText
                                                primary='Highest Educational Info'
                                                secondary={
                                                    formData.isHigherEducationInfoVisible
                                                        ? visibleText
                                                        : hiddenText
                                                }
                                            />
                                        </ListItem>
                                        <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                            <FormGroup>
                                                <FormControlLabel
                                                    control={<Checkbox checked={formData.isHigherEducationInfoVisible}
                                                        onChange={(e) => { setFormData({ ...formData, isHigherEducationInfoVisible: e.target.checked }); validateForm(); }} />}
                                                    label="Visible to members"
                                                />
                                                <FormControlLabel
                                                    control={<Checkbox checked={formData.isStudying}
                                                        onChange={(e) => { setFormData({ ...formData, isStudying: e.target.checked }); validateForm(); }} />}
                                                    label="Currently studying" />
                                            </FormGroup>
                                            <TextField
                                                label="University/College/School"
                                                onChange={(e) => { setFormData({ ...formData, university: e.target.value }); validateForm(); }}
                                                error={isFormTouched && !Validator.IsText(formData.university)}
                                                value={formData.university}
                                            />
                                            <TextField
                                                label="Degree/Level"
                                                onChange={(e) => { setFormData({ ...formData, degree: e.target.value }); validateForm(); }}
                                                error={isFormTouched && !Validator.IsText(formData.degree)}
                                                value={formData.degree}
                                            />
                                        </Stack>
                                    </>
                                )
                            }
                            {
                                formData.profileType && formData.profileType !== ProfileType.Student &&
                                (
                                    <>
                                        <Divider />
                                        <ListItem>
                                            <ListItemAvatar>
                                                <Avatar>
                                                    <WorkIcon />
                                                </Avatar>
                                            </ListItemAvatar>
                                            <ListItemText
                                                primary='Latest Employment Info'
                                                secondary={
                                                    formData.isEmploymentInfoVisible ? visibleText : hiddenText
                                                }
                                            />
                                        </ListItem>
                                        <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                            <FormGroup>
                                                <FormControlLabel
                                                    control={<Checkbox checked={formData.isEmploymentInfoVisible}
                                                        onChange={(e) => { setFormData({ ...formData, isEmploymentInfoVisible: e.target.checked }); }} />}
                                                    label="Visible to members"
                                                />
                                                <FormControlLabel
                                                    control={<Checkbox checked={formData.isWorking}
                                                        onChange={(e) => { setFormData({ ...formData, isWorking: e.target.checked }); }} />}
                                                    label="Currently working" />
                                            </FormGroup>
                                            <TextField
                                                label="Organization"
                                                onChange={(e) => setFormData({ ...formData, organization: e.target.value })}
                                                error={isFormTouched && formData.isWorking && !Validator.IsText(formData.organization)}
                                                value={formData.organization}
                                            />
                                            <TextField
                                                label="Job Title"
                                                onChange={(e) => setFormData({ ...formData, jobTitle: e.target.value })}
                                                error={isFormTouched && formData.isWorking && !Validator.IsText(formData.jobTitle)}
                                                value={formData.jobTitle}
                                            />
                                        </Stack>
                                    </>
                                )
                            }
                            <Divider />
                            <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                                <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Submit</Button>
                                {
                                    profile.status === CallStatus.Success && isSubmitted &&
                                    (<Alert severity="success">Successfully updated.</Alert>)
                                }
                                {
                                    !isSubmitted &&
                                    (<Alert severity="info">Please crosscheck your email before you submit.</Alert>)
                                }
                                {
                                    profile.status === CallStatus.Failure && isSubmitted &&
                                    (<Alert severity="error">Something went wrong. Try again.</Alert>)
                                }
                                <Backdrop
                                    sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                                    open={profile.status === CallStatus.InProgress}
                                >
                                    <CircularProgress color="inherit" />
                                </Backdrop>
                            </Stack>
                        </List>
                    </form>
                ) : (
                    <>
                        <Typography component="h4">Congratulations {profile.data.name} 🎉, your profile has been created!</Typography>
                        <Typography component="h5">Please <a href="/login">login</a> to continue.</Typography>
                    </>
                )}
            </PageCard>
        </>
    )
}
