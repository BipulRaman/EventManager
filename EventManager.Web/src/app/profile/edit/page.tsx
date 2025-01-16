"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ProfileUpdate } from './ProfileUpdate';
import { PageTabs } from '../../../components/PageTabs';
import { ProfileTabs } from '@/constants/Routes';

const ProfileEditPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ProfileTabs} />
            <ProfileUpdate />
        </PageWrapperAuth>
    );
};

export default ProfileEditPage;
