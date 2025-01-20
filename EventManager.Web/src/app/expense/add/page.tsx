"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ExpenseTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { ExpenseAdd } from './ExpenseAdd';

const ExpenseAddPage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ExpenseTabs} />
            <ExpenseAdd />
        </PageWrapperAuth>
    );
};

export default ExpenseAddPage;
