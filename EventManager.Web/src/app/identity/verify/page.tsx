"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { IdentityTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { IdentityVerify } from './IdentityVerify';

const IdentityVerifyPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={IdentityTabs} />
            <IdentityVerify />
        </PageWrapperAuth>
    );
};

export default IdentityVerifyPage;
