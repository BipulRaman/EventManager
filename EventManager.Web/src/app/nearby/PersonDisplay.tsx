"use client";
import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";
import { PageCard } from "../../components/PageCard";
import { ListItem, ListItemAvatar, ListItemText } from "@mui/material";
import { Person2Outlined } from "@mui/icons-material";
import HourglassBottomIcon from '@mui/icons-material/HourglassBottom';
import SchoolIcon from '@mui/icons-material/School';
import { PublicProfileResult } from "../../types/ProfileApiTypes";
import CallIcon from '@mui/icons-material/Call';
import PlaceIcon from '@mui/icons-material/Place';
import WorkIcon from '@mui/icons-material/Work';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import { ProfileType } from "@/types/ApiTypes";

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

export const PersonDisplay: React.FunctionComponent<PublicProfileResult> = (profile: PublicProfileResult) => {
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
              <span style={innerTextStyle}><PlaceIcon style={iconStyle} />{profile.isContactInfoVisible ? profile.location : "Location is Hidden"}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><CallIcon style={iconStyle} />{profile.isContactInfoVisible ? profile.phone : "Contact is Hidden"}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><SchoolIcon style={iconStyle} />{profile.isHigherEducationInfoVisible ? `${profile.degree} at ${profile.university}` : "Educational details are Hidden"}</span>
            </Typography><br />
            <Typography variant="body2" color="textSecondary" style={textStyle}>
              <span style={innerTextStyle}><WorkIcon style={iconStyle} />{profile.isEmploymentInfoVisible ? `${profile.jobTitle} at ${profile.organization}` : "Employment details are Hidden"}</span>
            </Typography><br />
          </div>
        </ListItem>
      </PageCard>
    </>
  );
};
