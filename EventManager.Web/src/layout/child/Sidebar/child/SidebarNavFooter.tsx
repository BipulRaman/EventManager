"use client";
import React from "react";
import { IconButton, Divider, Stack } from "@mui/material";
import LinkedInIcon from "@mui/icons-material/LinkedIn";
import TelegramIcon from "@mui/icons-material/Telegram";
import TwitterIcon from "@mui/icons-material/Twitter";
import GitHubIcon from "@mui/icons-material/GitHub";
import YouTubeIcon from "@mui/icons-material/YouTube";
import InstagramIcon from '@mui/icons-material/Instagram';
import FacebookIcon from '@mui/icons-material/Facebook';

const sideFooterStyle: React.CSSProperties = {
    position: "fixed",
    bottom: 0,
    paddingBottom: 10,
    backgroundColor: "#FFFFFF",
};

const sideFooterLineStyle: React.CSSProperties = {
    width: 250,
    marginBottom: 10,
};

export interface SidebarNavFooterProps {
    github: string;
    linkedIn: string;
    twitter: string;
    instagram: string;
    youTube: string;
    facebook: string;
    telegram: string;
}

export const SidebarNavFooter = (props: SidebarNavFooterProps) => {
    return (
        <div style={sideFooterStyle}>
            <Divider style={sideFooterLineStyle} />
            <Stack direction="row" alignItems="center" justifyContent={"space-evenly"}>
                {
                    props.github &&
                    (
                        <IconButton aria-label="GitHub profile link" component="a" href={props.github} target="_blank">
                            <GitHubIcon />
                        </IconButton>
                    )
                }
                {
                    props.linkedIn &&
                    (
                        <IconButton aria-label="LinkedIn profile link" component="a" href={props.linkedIn} target="_blank">
                            <LinkedInIcon />
                        </IconButton>
                    )
                }
                {
                    props.facebook &&
                    (
                        <IconButton aria-label="Facebook page link" component="a" href={props.facebook} target="_blank">
                            <FacebookIcon />
                        </IconButton>
                    )
                }
                {
                    props.twitter &&
                    (
                        <IconButton aria-label="Twitter profile link" component="a" href={props.twitter} target="_blank">
                            <TwitterIcon />
                        </IconButton>
                    )
                }
                {
                    props.instagram &&
                    (
                        <IconButton aria-label="Instagram profile link" component="a" href={props.instagram} target="_blank">
                            <InstagramIcon />
                        </IconButton>
                    )
                }
                {
                    props.youTube &&
                    (
                        <IconButton aria-label="YouTube channel link" component="a" href={props.youTube} target="_blank">
                            <YouTubeIcon />
                        </IconButton>
                    )
                }
                {
                    props.telegram &&
                    (
                        <IconButton aria-label="Telegram chat link" component="a" href={props.telegram} target="_blank">
                            <TelegramIcon />
                        </IconButton>
                    )
                }

            </Stack>
        </div>
    );
};
