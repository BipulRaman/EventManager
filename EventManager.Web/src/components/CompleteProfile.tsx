"use client";
import * as React from 'react';
import Typography from '@mui/material/Typography';
import { PageCard } from './PageCard';

export const CompleteProfile: React.FC = () => {
  return (
    <PageCard>
      <Typography gutterBottom variant="h6">
        Please complete your profile!
      </Typography>
      <Typography variant="body2" color="text.secondary">
        You can access the website features only when you complete your profile. After successful profile completion, You will be logged out automatically. Please login again to access the portal with all features.
      </Typography>
    </PageCard>
  );
}