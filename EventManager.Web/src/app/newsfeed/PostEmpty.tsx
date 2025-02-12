"use client";
import * as React from 'react';
import Typography from '@mui/material/Typography';
import { PageCard } from '../../components/PageCard';

export const PostEmpty = () => {
  return (
    <PageCard>
      <Typography gutterBottom variant="h5">
          It&apos;s empty here!
        </Typography>
        <Typography variant="body2" color="text.secondary">
          There are no posts under this category.
        </Typography>
    </PageCard>
  );
}