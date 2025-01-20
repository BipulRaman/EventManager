"use client";
import { NextPage } from 'next';
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ExpenseTabs } from '@/constants/Routes';
import { PageTabs } from '@/components/PageTabs';
import { ExpenseView } from './ExpenseView';

const ExpensePage: NextPage = () => {
    return (
        <PageWrapperAuth>
            <PageTabs tabs={ExpenseTabs} />
            <ExpenseView />
        </PageWrapperAuth>
    );
};

export default ExpensePage;
