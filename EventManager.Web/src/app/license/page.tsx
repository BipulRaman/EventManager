"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { License } from './License';

const BusinessPage: NextPage = () => {
    return (
        <PageWrapper>
            <License />
        </PageWrapper>
    );
};

export default BusinessPage;