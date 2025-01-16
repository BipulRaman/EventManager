"use client";
import { NextPage } from 'next';
import { PageWrapper } from '@/components/PageWrapper';
import { Register } from './Register';

const RegisterPage: NextPage = () => {
    return (
        <PageWrapper>
            <Register />
        </PageWrapper>
    );
};

export default RegisterPage;