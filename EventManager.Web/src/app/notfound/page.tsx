"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { NotFound } from './NotFound';

const NotFoundPage: NextPage = () => {
    return (
        <PageWrapper>
            <NotFound />
        </PageWrapper>
    );
};

export default NotFoundPage;