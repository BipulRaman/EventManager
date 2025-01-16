"use client";
import React from "react";
import { Card, CardActions, CardContent, CardHeader, IconButton, Stack, Typography } from "@mui/material";
import Link from "next/link";
import Avatar from '@mui/material/Avatar';
import BadgeIcon from '@mui/icons-material/Badge';
import { BusinessResult } from "@/types/BusinessApiTypes";
import WhatsAppIcon from '@mui/icons-material/WhatsApp';
import EmailIcon from '@mui/icons-material/Email';
import PhoneIcon from '@mui/icons-material/Phone';
import PlaceIcon from '@mui/icons-material/Place';
import LanguageIcon from '@mui/icons-material/Language';

const titleStyle: React.CSSProperties = {
    fontSize: 16,
    color: "#454545",
    marginTop: -10,
    paddingBottom: 5,
}

const cardContentStyle: React.CSSProperties = {
    paddingTop: 0,
    paddingBottom: 0,
}

export const NearByBusinessDisplay = (props: BusinessResult) => {
    return (
        <>
            <Card sx={{ mb: 1 }}>
                <CardHeader
                    avatar={
                        <Avatar>
                            <BadgeIcon />
                        </Avatar>
                    }                    
                    title={<span style={titleStyle}>{props.name}</span>}
                    subheader={<Typography variant="body2" color="textSecondary">[{props.category}]</Typography>}
                />
                <CardContent style={cardContentStyle}>
                    <Stack spacing={1}>
                        <Typography variant="body2" color="textSecondary">
                            <b>Address: </b>{props.address}, {props.pinCode}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Details: </b>{props.details}
                        </Typography>
                        <Typography variant="body2" color="textSecondary">
                            <b>Phone: </b>{props.phone}
                        </Typography>
                        {
                            props.email && (
                                <Typography variant="body2" color="textSecondary">
                                    <b>Email: </b>{props.email}
                                </Typography>
                            )
                        }
                        {
                            props.website && (
                                <Typography variant="body2" color="textSecondary">
                                    <b>Website: </b>{props.website}
                                </Typography>
                            )
                        }
                        <Typography variant="body2" color="textSecondary">
                            <b>Owner: </b>
                            <Link href={`/profile/#${props.createdBy}`} passHref>
                                {props.createdByName}
                            </Link>
                        </Typography>
                    </Stack>
                </CardContent>
                <CardActions disableSpacing>
                    {
                        props.website && (
                            <IconButton aria-label="open website" href={props.website} target="_blank" rel="noopener noreferrer">
                                <LanguageIcon />
                            </IconButton>
                        )
                    }
                    {
                        props.email && (
                            <IconButton aria-label="send email" href={`mailto:${props.email}`}>
                                <EmailIcon />
                            </IconButton>
                        )
                    }
                    <IconButton aria-label="open in map" href={props.mapLink} target="_blank" rel="noopener noreferrer">
                        <PlaceIcon />
                    </IconButton>
                    <IconButton aria-label="make phone call" href={`tel:${props.phone}`}>
                        <PhoneIcon />
                    </IconButton>
                    <IconButton aria-label="message on whatsapp" href={`https://wa.me/91${props.phone}`} target="_blank" rel="noopener noreferrer">
                        <WhatsAppIcon />
                    </IconButton>
                </CardActions>
            </Card>
        </>
    );
};