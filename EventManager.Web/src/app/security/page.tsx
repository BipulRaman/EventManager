"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { SecurityTabs } from '@/constants/Routes';
import { OfflineOtp } from './OfflineOtp';
import { PageTabs } from '@/components/PageTabs';

const SecurityPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={SecurityTabs} />
            <OfflineOtp />
        </PageWrapperAuth>
    );
};

export default SecurityPage;
