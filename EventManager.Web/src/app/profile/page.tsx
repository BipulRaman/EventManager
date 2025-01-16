"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ProfileTabs } from '@/constants/Routes';
import { ProfileView } from './ProfileView';
import { PageTabs } from '@/components/PageTabs';

const ProfilePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ProfileTabs} />
            <ProfileView />
        </PageWrapperAuth>
    );
};

export default ProfilePage;
