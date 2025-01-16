"use client";
import { PageWrapperAuth } from '@/components/PageWrapperAuth';
import { ProfileInfoUpdate } from './ProfileInfoUpdate';
import { CompleteProfile } from '@/components/CompleteProfile';
import { ProfilePhotoUpdate } from './ProfilePhotoUpdate';
import { GetUserRoles } from '@/utils/TokenHelper';
import { Roles } from '@/types/ApiTypes';

export const ProfileUpdate: React.FunctionComponent = () => {
    const userRoles = GetUserRoles();
    const isInvitedUser = userRoles.includes(Roles.InvitedUser);
    return (
        <PageWrapperAuth>
            {isInvitedUser ? <CompleteProfile /> : null}
            <ProfilePhotoUpdate /><ProfileInfoUpdate />
        </PageWrapperAuth>
    );
}