"use client";
import { IconButton } from "@mui/material";
import React, { useContext } from "react";
import Fingerprint from "@mui/icons-material/Fingerprint";
import { useRouter } from 'next/navigation';
import { AuthContext } from "@/utils/AuthContextHelper";

export const LoginLogoutButton: React.FunctionComponent = () => {
  const { isAuthenticated } = useContext(AuthContext)

  const router = useRouter();

  const onLoginClick = () => {
    router.push('/login');
  };

  const onLogoutClick = () => {
    router.push('/logout');
  };

  const logoutButtonStyle: React.CSSProperties = {
    color: "#00ff00",
  }

  const loginButtonStyle: React.CSSProperties = {
    color: "#ffd700",
  }

  return (
    <>
      {isAuthenticated ? (
        <>
          <IconButton aria-label="logout" style={logoutButtonStyle} onClick={onLogoutClick}>
            <Fingerprint />
          </IconButton>
        </>
      ) : (
        <>
          <IconButton aria-label="login" style={loginButtonStyle} color="secondary" onClick={onLoginClick}>
            <Fingerprint />
          </IconButton>
        </>
      )}
    </>
  );
};
