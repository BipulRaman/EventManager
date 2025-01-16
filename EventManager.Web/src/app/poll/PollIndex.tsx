"use client";
import * as React from 'react';
import { UnderDevelopment } from '../../components/UnderDevelopment';
import { TabbedComponent, TabbedComponents } from '../../components/TabbedComponents';

export const PollIndex = () => {
    const tabbedComponents: TabbedComponent[] = [
        {
            Position: 0,
            Label: "Poll",
            Component: <UnderDevelopment />
        }
    ]

    return (
        <>
            <TabbedComponents Components={tabbedComponents} />
        </>
    );
}