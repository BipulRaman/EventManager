"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { PrivacyPolicy } from './PrivacyPolicy';

const PrivacyPage: NextPage = () => {
    return (
        <PageWrapper>
            <PrivacyPolicy />
        </PageWrapper>
    );
};

export default PrivacyPage;