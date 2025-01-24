"use client";
import React from 'react'
import { Button, Checkbox, Chip, Divider, FormControl, FormControlLabel, FormGroup, InputLabel, MenuItem, Select, SelectChangeEvent, Stack, TextField, Typography } from '@mui/material'
import List from '@mui/material/List'
import ListItem from '@mui/material/ListItem'
import ListItemText from '@mui/material/ListItemText'
import ListItemAvatar from '@mui/material/ListItemAvatar'
import Avatar from '@mui/material/Avatar'
import { PageCard } from '../../../components/PageCard'
import ContactMailIcon from '@mui/icons-material/ContactMail'
import WorkIcon from '@mui/icons-material/Work'
import SchoolIcon from '@mui/icons-material/School'
import BadgeIcon from '@mui/icons-material/Badge';
import { UpdateProfilePayload, CastedUpdateProfilePayload } from '../../../types/ProfileApiTypes'
import { CallStatus, Gender, ProfileType, SchoolData, Validator } from '../../../types/ApiTypes'
import { Schools } from '../../../constants/StaticData'
import { GetUserEmail } from '../../../utils/TokenHelper'
import useGlobalState from '@/state/GlobalState'
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';
import { ProfileServices } from '@/services/ServicesIndex';
import { StatusMessage } from '@/components/StatusMessage';

const submitButtonStyle: React.CSSProperties = {
    width: 150,
}

export const ProfileInfoUpdate: React.FunctionComponent = () => {
    const hiddenText = '[Visible to you only]'
    const visibleText = '[Visible to all members]'
    const { profileState, setProfileState } = useGlobalState();
    const [formData, setFormData] = React.useState<UpdateProfilePayload>({} as UpdateProfilePayload);
    const [isFormValid, setIsFormValid] = React.useState<boolean>(false);
    const [isSubmitted, setIsSubmitted] = React.useState<boolean>(false);
    const [selectedSchools, setSelectedSchools] = React.useState<SchoolData[]>([]);

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

    const populateSelectedSchools = () => {
        if (profileState.data.secondarySchoolIds) {
            const newSecondarySchools = profileState.data.secondarySchoolIds.map((uniqueId) => Schools.find((school) => school.UniqueId === uniqueId) as SchoolData);
            setSelectedSchools(newSecondarySchools);
        }
    }

    React.useEffect(() => {
        if (profileState.status === CallStatus.Success && profileState.data.id) {
            setFormData(CastedUpdateProfilePayload(profileState.data));
            populateSelectedSchools();
        }
        else {
            ApiGlobalStateManager(ProfileServices.GetProfile(), setProfileState).then(() => { populateSelectedSchools() });
        }
    }, []);

    React.useEffect(() => {
        setFormData(CastedUpdateProfilePayload(profileState.data));
    }, [formData.id, profileState.data])

    // Validate the Update Profile Payload
    const validateForm = () => {
        const isGeneralInfoValid: boolean = Validator.IsText(formData.name)
            && Validator.IsPhone(formData.phone)
            && Validator.IsText(formData.location)
            && Validator.IsText(formData.university)
            && Validator.IsText(formData.degree)

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
            && Validator.IsText(formData.organization)
            && Validator.IsText(formData.jobTitle);

        if (formData.profileType === ProfileType.Student) {
            setIsFormValid(isStudentInfoValid);
            return isGeneralInfoValid;
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
        if (validateForm()) {
            setIsSubmitted(true);
            ApiGlobalStateManager(ProfileServices.UpdateProfile(formData), setProfileState);
        }
    }

    return (
        <PageCard>
            {formData.id ? (
                <form onSubmit={handleFormSubmit} onChange={validateForm}>
                    {isSubmitted && (
                        <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                            <StatusMessage
                                notStartedMessage="Go ahead, Update your info👍"
                                successMessage="Successfully updated."
                                failureMessage="Something went wrong. Try again."
                                currentStatus={profileState.status}
                            />
                        </Stack>
                    )}
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
                                error={formData.name !== '' && formData.name !== null && formData.name.length < 3}
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
                                    value={formData.primarySchoolId}
                                    label="Entry JNV"
                                    required
                                    onChange={(e) => { setFormData({ ...formData, primarySchoolId: Number(e.target.value) }); validateForm(); }}
                                >
                                    {Schools.map((school) => (
                                        <MenuItem key={school.UniqueId} value={school.UniqueId}>[{school.StatePrefix}] JNV {school.SchoolName}</MenuItem>
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
                                    onChange={(e) => { handleSecondarySchoolIdsAdd(Schools.find((school) => school.UniqueId === Number(e.target.value)) as SchoolData); e.target.value = ''; validateForm(); }}
                                >
                                    <MenuItem disabled value="Add other JNVs attended">Add other JNVs attended</MenuItem>
                                    {Schools.map((school) => (
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
                                            error={formData.profileType === ProfileType.Student ? false : formData.exitClass <= formData.entryClass}
                                            onChange={(e) => { setFormData({ ...formData, entryClass: parseInt(e.target.value) }); validateForm(); }}
                                        >
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
                                            label="Entry class"
                                            required
                                            error={formData.exitClass <= formData.entryClass}
                                            onChange={(e) => { setFormData({ ...formData, exitClass: parseInt(e.target.value) }); validateForm(); }}
                                        >
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
                                    value={formData.entryYear.toString()}
                                    label="Entry class"
                                    required
                                    error={formData.profileType === ProfileType.Student || formData.profileType === ProfileType.Staff ? false : formData.exitYear <= formData.entryYear}
                                    onChange={(e) => { setFormData({ ...formData, entryYear: parseInt(e.target.value) }); validateForm(); }}
                                >
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
                                            value={formData.exitYear.toString()}
                                            label="Entry class"
                                            error={formData.exitYear <= formData.entryYear}
                                            required
                                            onChange={(e) => { setFormData({ ...formData, exitYear: parseInt(e.target.value) }); validateForm(); }}
                                        >
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
                                disabled
                                value={profileState.data.email ?? GetUserEmail()}
                            />
                            <TextField
                                label="Phone"
                                onChange={(e) => { setFormData({ ...formData, phone: e.target.value }); validateForm(); }}
                                error={!Validator.IsPhone(formData.phone)}
                                value={formData.phone}
                            />
                            <TextField
                                label="Location"
                                onChange={(e) => { setFormData({ ...formData, location: e.target.value }); validateForm(); }}
                                error={!Validator.IsText(formData.location)}
                                value={formData.location}
                            />
                        </Stack>
                        {formData.profileType && formData.profileType !== ProfileType.Student &&
                            (
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
                                                label="Visible to members" />
                                            <FormControlLabel
                                                control={<Checkbox checked={formData.isStudying}
                                                    onChange={(e) => { setFormData({ ...formData, isStudying: e.target.checked }); validateForm(); }} />}
                                                label="Currently studying" />
                                        </FormGroup>
                                        <TextField
                                            label="University/College/School"
                                            onChange={(e) => { setFormData({ ...formData, university: e.target.value }); validateForm(); }}
                                            error={!Validator.IsText(formData.university)}
                                            value={formData.university}
                                        />
                                        <TextField
                                            label="Degree/Level"
                                            onChange={(e) => { setFormData({ ...formData, degree: e.target.value }); validateForm(); }}
                                            error={!Validator.IsText(formData.degree)}
                                            value={formData.degree}
                                        />
                                    </Stack>
                                </>)
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
                                                    onChange={(e) => { setFormData({ ...formData, isEmploymentInfoVisible: e.target.checked }); validateForm(); }} />}
                                                label="Visible to members" />
                                            <FormControlLabel
                                                control={<Checkbox checked={formData.isWorking}
                                                    onChange={(e) => { setFormData({ ...formData, isWorking: e.target.checked }); validateForm(); }} />}
                                                label="Currently working" />
                                        </FormGroup>
                                        <TextField
                                            label="Organization"
                                            onChange={(e) => setFormData({ ...formData, organization: e.target.value })}
                                            error={formData.isWorking ? !Validator.IsText(formData.organization) : false}
                                            value={formData.organization}
                                        />
                                        <TextField
                                            label="Job Title"
                                            onChange={(e) => { setFormData({ ...formData, jobTitle: e.target.value }); validateForm(); }}
                                            error={formData.isWorking ? !Validator.IsText(formData.jobTitle) : false}
                                            value={formData.jobTitle}
                                        />
                                    </Stack>
                                </>
                            )
                        }
                        <Divider />
                        <Stack direction='column' spacing={2} margin={2} maxWidth={400}>
                            <Button style={submitButtonStyle} disabled={!isFormValid} variant="contained" type="submit">Update</Button>
                            {
                                isSubmitted && (
                                    <StatusMessage
                                        notStartedMessage="Go ahead, Update your info👍"
                                        successMessage="Successfully updated."
                                        failureMessage="Something went wrong. Try again."
                                        currentStatus={profileState.status}
                                    />
                                )
                            }
                        </Stack>
                    </List>
                </form>
            ) : (
                <Typography component="p">Loading...</Typography>
            )}
        </PageCard>
    )
}
