"use client";
import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";
import { PageCard } from "../../components/PageCard";
import { Button, ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { Person2Outlined } from "@mui/icons-material";
import HourglassBottomIcon from '@mui/icons-material/HourglassBottom';
import SchoolIcon from '@mui/icons-material/School';
import { ProfileResult } from "../../types/ProfileApiTypes";
import CallIcon from '@mui/icons-material/Call';
import PlaceIcon from '@mui/icons-material/Place';
import WorkIcon from '@mui/icons-material/Work';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import { ProfileType } from "@/types/ApiTypes";
import { StatusMessage } from "@/components/StatusMessage";
import { StateData } from "@/state/GlobalState";
import { ProfileServices } from "@/services/ServicesIndex";
import { ApiComponentStateManager } from "@/utils/ServiceStateHelper";
import HowToRegIcon from '@mui/icons-material/HowToReg';

const titleStyle: React.CSSProperties = {
  fontSize: "1.3rem",
  fontWeight: "400",
};

const iconStyle: React.CSSProperties = {
  fontSize: "1.2rem",
  fontWeight: "400",
  marginRight: "0.5rem",
};

const textStyle: React.CSSProperties = {
  display: 'inline-flex',
  alignItems: 'center',
}

const innerTextStyle: React.CSSProperties = {
  display: 'inline-flex',
  verticalAlign: 'top',
  whiteSpace: 'normal',
  wordWrap: 'break-word',
}

export const PersonDisplay: React.FunctionComponent<ProfileResult> = (profile: ProfileResult) => {
  const [checkedIn, setCheckedIn] = React.useState<StateData<boolean>>({} as StateData<boolean>);

  const checkInVenue = () => {
    ApiComponentStateManager(ProfileServices.CheckInVenue(profile.id), setCheckedIn);
  }

  return (
    <>
      <PageCard>
        <ListItem>
          <ListItemAvatar>
            <Avatar>
              <Person2Outlined />
            </Avatar>
          </ListItemAvatar>
          <ListItemText primary={<span style={titleStyle}>{profile.name}</span>} />
        </ListItem>
        <ListItem>
          <div>
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              {
                profile.profileType === ProfileType.Alumni && (
                  <span style={innerTextStyle}><HourglassBottomIcon style={iconStyle} />{profile.profileType} | {`${profile.entryYear} - ${profile.exitYear} | Class ${profile.entryClass} - ${profile.exitClass}`}</span>
                )
              }
              {
                profile.profileType === ProfileType.Student && (
                  <span style={innerTextStyle}><HourglassBottomIcon style={iconStyle} />{profile.profileType} | {`${profile.entryYear} - Present | Class ${profile.entryClass} - Onwards`}</span>
                )
              }
              {
                profile.profileType === ProfileType.Staff && (
                  <span style={innerTextStyle}><HourglassBottomIcon style={iconStyle} />{profile.profileType} | {`${profile.entryYear} - Present`}</span>
                )
              }
              {
                profile.profileType === ProfileType.ExStaff && (
                  <span style={innerTextStyle}><HourglassBottomIcon style={iconStyle} />{profile.profileType} | {`${profile.entryYear} - ${profile.exitYear}`}</span>
                )
              }
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><LocalLibraryIcon style={iconStyle} />{profile.secondarySchoolNames ? `${profile.primarySchoolName}, ${profile.secondarySchoolNames}` : profile.primarySchoolName}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><PlaceIcon style={iconStyle} />{profile.location}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><CallIcon style={iconStyle} />{profile.phone}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><SchoolIcon style={iconStyle} />{`${profile.degree} at ${profile.university}`}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><WorkIcon style={iconStyle} />{`${profile.jobTitle} at ${profile.organization}`}</span>
            </Typography><br />
            {
              profile.venueCheckInDateTime && (
                <>
                  <Typography variant="body2" color="textSecondary" style={textStyle}>
                    <span style={innerTextStyle}><HowToRegIcon style={iconStyle} />{`Venue @ ${new Date(profile.venueCheckInDateTime).toLocaleString('en-US', { month: 'short', day: '2-digit', year: 'numeric', hour: '2-digit', minute: '2-digit', hour12: false })}`}</span>
                  </Typography><br />
                </>
              )
            }
          </div>
        </ListItem>
        <div style={{ margin: '1rem' }}>
          <Button disabled={!!profile.venueCheckInDateTime} variant="contained" size="small" style={{ textTransform: 'none' }} onClick={checkInVenue}>
            Check-In Venue
          </Button>
          <StatusMessage
            notStartedMessage="Please enter phone number and click search."
            successMessage={`${profile.name} checked in at venue.`}
            failureMessage="Something went wrong."
            currentStatus={checkedIn.status}
          />
        </div>
      </PageCard>
    </>
  );
};
