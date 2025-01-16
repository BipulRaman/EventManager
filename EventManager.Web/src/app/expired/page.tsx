"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { Expired } from './Expired';

const ExpiredPage: NextPage = () => {
    return (
        <PageWrapper>
            <Expired />
        </PageWrapper>
    );
};

export default ExpiredPage;