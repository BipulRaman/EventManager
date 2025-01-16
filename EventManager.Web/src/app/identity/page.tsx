"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { IdentityTabs } from '@/constants/Routes';
import { IdentityView } from './IdentityView';
import { PageTabs } from '@/components/PageTabs';

const IdentityPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={IdentityTabs} />
            <IdentityView />
        </PageWrapperAuth>
    );
};

export default IdentityPage;
