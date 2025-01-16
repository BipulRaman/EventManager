"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { Disclaimer } from './Disclaimer';

const DisclaimerPage: NextPage = () => {
    return (
        <PageWrapper>
            <Disclaimer />
        </PageWrapper>
    );
};

export default DisclaimerPage;