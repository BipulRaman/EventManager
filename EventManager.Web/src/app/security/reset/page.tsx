"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { PageTabs } from '../../../components/PageTabs';
import { SecurityTabs } from '@/constants/Routes';
import { ResetAuth } from './ResetAuth';

const SecurityResetPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={SecurityTabs} />
            <ResetAuth />
        </PageWrapperAuth>
    );
};

export default SecurityResetPage;
