"use client";
import * as React from 'react';
import { PageCard } from '../../components/PageCard';
import { PersonDisplay } from './PersonDisplay';
import { Button, Divider, Input, Paper, Typography } from '@mui/material';
import { ProfileResult } from '../../types/ProfileApiTypes';
import { ApiComponentStateManager } from '@/utils/ServiceStateHelper';
import { ProfileServices } from '@/services/ServicesIndex';
import { CallStatus } from '@/types/ApiTypes';
import { StateData } from '@/state/GlobalState';
import { StatusMessage } from '@/components/StatusMessage';
import PersonSearchIcon from '@mui/icons-material/PersonSearch';
import { Validator } from '../../types/ApiTypes';

export const CheckinVenue: React.FC = () => {
  const [profileList, setProfileList] = React.useState<StateData<ProfileResult[]>>({} as StateData<ProfileResult[]>);
  const [isTouch, setIsTouch] = React.useState<boolean>(false);
  const [phone, setPhone] = React.useState<string>("");

  const fetchUsersByPhone = () => {
    ApiComponentStateManager(ProfileServices.GetProfilesByPhone(phone), setProfileList);
    setPhone("");
    setIsTouch(false);
  }

  return (
    <>
      <PageCard>
        <Typography variant="h6" component="h2" color="textSecondary" gutterBottom>
          Venue Check-In
        </Typography>
        <Paper
          component="form"
          sx={{ padding: '5px 0px', display: 'flex', alignItems: 'center', width: "100%", maxWidth: "400px" }}
          onSubmit={(e) => { e.preventDefault(); fetchUsersByPhone(); }}          
        >
          <Input
            sx={{ ml: 1, flex: 1 }}
            placeholder="Enter phone number"
            value={phone}
            onChange={(e) => { setPhone(e.target.value); setIsTouch(true); }}
            error={isTouch && !Validator.IsPhone(phone)}
          />
          <Divider sx={{ height: 28, m: 0.5 }} orientation="vertical" />
          <Button onClick={fetchUsersByPhone} endIcon={<PersonSearchIcon />}></Button>
        </Paper>
        <StatusMessage 
          display= {false}
          notStartedMessage= "Please enter phone number and click search."
          successMessage={`${profileList.data?.length || 0} result(s) found${profileList.data?.length ? ` for ${profileList.data[0]?.phone}` : ''}.`}
          failureMessage="Something went wrong."
          currentStatus={profileList.status}
         />    
      </PageCard>
      {
        profileList.status === CallStatus.Success && profileList.data && profileList.data.length > 0 && (
          profileList.data.map((profile, index) => (
            <PersonDisplay key={index} {...profile} />
          ))
        )
      }
    </>
  );
}
