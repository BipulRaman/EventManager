"use client";
import { NextPage } from 'next';
import { Login } from './Login';
import { PageWrapper } from '@/components/PageWrapper';

const LoginPage: NextPage = () => {
    return (
        <PageWrapper>
            <Login />
        </PageWrapper>
    );
};

export default LoginPage;
