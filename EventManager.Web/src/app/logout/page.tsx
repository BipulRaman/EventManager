"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { Logout } from './Logout';

const LogoutPage: NextPage = () => {
    return (
        <PageWrapper>
            <Logout />
        </PageWrapper>
    );
};

export default LogoutPage;