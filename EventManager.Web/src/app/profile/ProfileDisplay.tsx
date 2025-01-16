"use client";
import React from "react";
import { Divider, IconButton, Typography } from "@mui/material";
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import ContactMailIcon from '@mui/icons-material/ContactMail';
import WorkIcon from '@mui/icons-material/Work';
import SchoolIcon from '@mui/icons-material/School';
import BadgeIcon from '@mui/icons-material/Badge';
import { GetUserEmail } from "../../utils/TokenHelper";
import { ProfileResult } from "../../types/ProfileApiTypes";
import { Pathname } from "../../constants/Routes";
import ContentCopyIcon from '@mui/icons-material/ContentCopy';
import Link from "next/link";

const profileLinkStyle: React.CSSProperties = {
    cursor: "pointer",
    paddingTop: "10px",
}

export const ProfileDisplay = (props: ProfileResult) => {
    const hiddenText = '[NOT visible to anyone]';
    const visibleText = '[Visible to all members]';

    return (
        <>
            <List >
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <BadgeIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Navodaya Info" secondary={visibleText} />
                </ListItem>
                <ListItem>
                    <div>
                        <Typography variant="body2" color="textSecondary">
                            <b>Name:</b> {props.name}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>{props.profileType}:</b> {props.entryYear} - {props.exitYear === 0 ? "Present" : props.exitYear}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Association:</b> {props.primarySchoolName}{props.secondarySchoolNames ? `, ${props.secondarySchoolNames}` : ""}
                        </Typography>
                    </div>
                </ListItem>
                <Divider />
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <ContactMailIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Contact Info" secondary={props.isContactInfoVisible ? visibleText : hiddenText} />
                </ListItem>
                <ListItem>
                    <div>
                        <Typography variant="body2" color="textSecondary">
                            <b>Email:</b> {props.email ?? GetUserEmail()}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Phone:</b> {props.phone}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Location:</b> {props.location}
                        </Typography>
                    </div>
                </ListItem>
                <Divider />
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <SchoolIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Education Info" secondary={props.isHigherEducationInfoVisible ? visibleText : hiddenText} />
                </ListItem>
                <ListItem>
                    <div>
                        <Typography variant="body2" color="textSecondary">
                            <b>University:</b> {props.university}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Degree:</b> {props.degree}
                        </Typography>
                    </div>
                </ListItem>
                <Divider />
                <ListItem>
                    <ListItemAvatar>
                        <Avatar>
                            <WorkIcon />
                        </Avatar>
                    </ListItemAvatar>
                    <ListItemText primary="Work Info" secondary={props.isEmploymentInfoVisible ? visibleText : hiddenText} />
                </ListItem>
                <ListItem>
                    <div>
                        <Typography variant="body2" color="textSecondary">
                            <b>Job Title:</b> {props.jobTitle}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Organization:</b> {props.organization}
                        </Typography>
                    </div>
                </ListItem>
                <Divider />
                <ListItem>
                    <div style={profileLinkStyle}><b>Profile Link</b>: <Link href={`${Pathname.Profile}/#${props.id}`}>{getProfileLink(props.id)}</Link>
                        <IconButton aria-label="delete" size="small" onClick={() => copyProfileLinkToClipboard(props.id)}>
                            <ContentCopyIcon fontSize="inherit" />
                        </IconButton>
                    </div>
                </ListItem>
            </List>
        </>
    );
};

const getProfileLink = (profileId: string) => {
    return `${window.location.origin}${Pathname.Profile}/#${profileId}`;
}

const copyProfileLinkToClipboard = (profileId: string) => {
    const profileLink = getProfileLink(profileId);
    navigator.clipboard.writeText(profileLink);
}
