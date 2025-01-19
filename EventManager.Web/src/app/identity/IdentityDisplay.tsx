"use client";
import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import { Avatar, Container, Divider, Stack } from '@mui/material';
import QRCode from 'react-qr-code';
import { PublicProfileResult } from '../../types/ProfileApiTypes';
import SchoolIcon from '@mui/icons-material/School';
import LocalLibraryIcon from '@mui/icons-material/LocalLibrary';
import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';

const avatarContainerStyle: React.CSSProperties = {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    minHeight: "fit-content",
    backgroundColor: "#85c9e9",
    padding: 20,
};

const avatarStyle: React.CSSProperties = {
    maxWidth: 300,
    width: "75%",
    maxHeight: 300,
    height: "75%",
    aspectRatio: "1/1",
};

const idCardStyle: React.CSSProperties = {
    maxWidth: 300,
};

const userIdStyle: React.CSSProperties = {
    fontSize: 10,
    color: "#666666",
};

const nameTextStyle: React.CSSProperties = {
    fontSize: 20,
    paddingBottom: 5,
    color: "#666666",
};

const metaTextStyle: React.CSSProperties = {
    display: 'inline-flex',
    fontSize: 13,
    color: "#666666",
    lineHeight: 1.5
};

const iconStyle: React.CSSProperties = {
    fontSize: "1.2rem",
    fontWeight: "400",
    marginRight: "0.5rem",
};

export const IdentityDisplay = (props: PublicProfileResult) => {

    const getTimeStamp = (): string => {
        const currentDate = new Date();

        const year = currentDate.getFullYear().toString();
        const month = padZero(currentDate.getMonth() + 1); // Months are zero-based
        const day = padZero(currentDate.getDate());
        const hours = padZero(currentDate.getHours());
        const minutes = padZero(currentDate.getMinutes());
        const seconds = padZero(currentDate.getSeconds());

        return `${year}${month}${day}${hours}${minutes}${seconds}`;
    }

    const padZero = (value: number): string => {
        return value < 10 ? `0${value}` : value.toString();
    }

    return (
        <Card style={idCardStyle}>
            <Container style={avatarContainerStyle}>
                <Avatar alt="Person" style={avatarStyle} src={props.photo} />
            </Container>
            <Divider />
            <CardContent>
                <div style={nameTextStyle}>{props.photo ? props.name : "[ Upload Photo ]"}</div>
                {/* <div style={userIdStyle}>https://nvsalumni.in | Navodaya Alumni Network</div> */}
                <div style={userIdStyle}>{props.id}</div>
                <Stack direction="row" justifyContent={'flex-start'} spacing={1} paddingTop={1}>
                    <div>
                        <div style={{ height: "auto", minWidth: 75, width: "100%" }}>
                            <QRCode
                                size={75}
                                style={{ height: "auto", maxWidth: "100" }}
                                value={props.id}
                                viewBox={`0 0 100 100`}
                            />
                        </div>
                        <div style={userIdStyle}>{getTimeStamp()}</div>
                    </div>
                    <div>
                        <Stack>
                            <div style={metaTextStyle}>
                                <AssignmentIndIcon style={iconStyle} /> {props.profileType}
                            </div>
                            <div style={metaTextStyle}>
                                <LocalLibraryIcon style={iconStyle} /> {props.entryYear === 0 ? "Present" : props.entryYear} - {props.exitYear === 0 ? "Present" : props.exitYear}
                            </div>
                            <div style={metaTextStyle}>
                                <SchoolIcon style={iconStyle} />{props.primarySchoolName}{props.secondarySchoolNames ? ", " + props.secondarySchoolNames : ""}
                            </div>
                        </Stack>
                    </div>
                </Stack>
            </CardContent>
        </Card>
    );
}