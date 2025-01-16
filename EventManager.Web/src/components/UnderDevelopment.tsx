"use client";
import * as React from 'react';
import Typography from '@mui/material/Typography';
import { PageCard } from './PageCard';

export const UnderDevelopment: React.FC = () => {
  return (
    <PageCard>
      <Typography gutterBottom variant="h5">
          Coming soon!
        </Typography>
        <Typography variant="body2" color="text.secondary">
          We are currently working on this feature to make it available soon. Until then, you can check out other features.
        </Typography>
    </PageCard>
  );
}