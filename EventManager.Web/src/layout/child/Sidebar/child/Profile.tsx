"use client";
import React from "react";
import { Avatar, Typography } from "@mui/material";

const rootStyle: React.CSSProperties = {
  display: "flex",
  flexDirection: "column",
  alignItems: "center",
  minHeight: "fit-content",
  paddingBottom: 15,
};

const avatarStyle: React.CSSProperties = {
  width: 80,
  height: 80,
};

const avatarTitle: React.CSSProperties = {
  marginTop: 10,
}

export interface ProfileProps {
  name: string;
  avatar: string;
  bio: string;
}

export const Profile = (props: ProfileProps) => {

  return (
    <div style={rootStyle}>
      <Avatar alt="Person" style={avatarStyle} src={props.avatar} />
      <Typography component={"div"} variant="h5" style={avatarTitle}>{props.name}</Typography>
      <Typography component={"div"} variant="body2" color="textSecondary">
        {props.bio}
      </Typography>
    </div>
  );
};
