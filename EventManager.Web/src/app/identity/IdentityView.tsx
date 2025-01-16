"use client";
import * as React from 'react';
import { CallStatus } from '../../types/ApiTypes';
import { IdentityDisplay } from './IdentityDisplay';
import { Button, Typography } from '@mui/material';
import { toJpeg } from 'html-to-image';
import DownloadIcon from '@mui/icons-material/Download';
import { PageCard } from '../../components/PageCard';
import useGlobalState from '../../state/GlobalState';
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';
import { ProfileServices } from '@/services/ServicesIndex';
import Link from 'next/link';

export const IdentityView: React.FC = () => {
    const { profileState, setProfileState } = useGlobalState();

    const handleDownload = () => {
        try {
            const node = document.getElementById('alumniId')?.children[0] as HTMLElement;
            if (node) {
                toJpeg(node, { quality: 0.95 })
                    .then(function (dataUrl) {
                        const link = document.createElement('a') as HTMLAnchorElement;
                        link.download = `Alumni_Id_${profileState.data.id}.jpeg`;
                        link.href = dataUrl;
                        link.click();
                    });
            }
        }
        catch {
        }
    }

    React.useEffect(() => {
        if (!profileState.data.id) {
            ApiGlobalStateManager(ProfileServices.GetProfile(), setProfileState);
        }
    }, [])

    switch (profileState.status) {
        case CallStatus.Success:
            return (
                <>
                    {
                        (profileState.status === CallStatus.Success && profileState.data) ? (
                            <div>
                                <div id="alumniId"><IdentityDisplay {...profileState.data} /></div>
                                <br />
                                {
                                    profileState.data.photo ? (
                                        <Button variant="contained" color="primary" onClick={handleDownload} endIcon={<DownloadIcon />}>Download</Button>
                                    ) : (
                                        <Typography variant="body2" color="text.secondary">Please complete your profile with a photo to view or download your <b>Digital ID Card</b>. Upload your profile now at <Link href="/profile/edit" >Profile Update</Link> page.</Typography>
                                    )
                                }                                
                            </div>
                        ) : (
                            <PageCard>
                                <Typography variant="body2" color="text.secondary">Please complete your profile with a photo to view or download your <b>Digital ID Card</b>. Upload your profile now at <Link href="/profile/edit" >Profile Update</Link> page.</Typography>
                            </PageCard>
                        )
                    }
                </>
            );
        case CallStatus.Failure:
            return (
                <PageCard>
                    <Typography variant="body2" color="text.secondary">Error loading profile</Typography>
                </PageCard>
            );
        default:
            return (
                <PageCard>
                    <Typography variant="body2" color="text.secondary">Loading...</Typography>
                </PageCard>
            );
    }
}