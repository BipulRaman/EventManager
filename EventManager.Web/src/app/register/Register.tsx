"use client";
import React from "react";
import { ProfileCreate } from "../profile/ProfileCreate";

export const Register: React.FunctionComponent = () => {
  return (
    <div style={{ display: 'flex', justifyContent: 'center' }}>
      <ProfileCreate />
    </div>
  );
};
