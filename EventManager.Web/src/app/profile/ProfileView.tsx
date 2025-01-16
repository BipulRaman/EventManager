"use client";
import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";
import { ProfileDisplay } from "./ProfileDisplay";
import useGlobalState, { StateData } from "../../state/GlobalState";
import { ProfileResult } from "../../types/ProfileApiTypes";
import { ApiComponentStateManager, ApiGlobalStateManager } from "@/utils/ServiceStateHelper";
import { ProfileServices } from "@/services/ServicesIndex";

export const ProfileView: React.FunctionComponent = () => {
    const { profileState, setProfileState } = useGlobalState();
    const [isOtherProfile, setIsOtherProfile] = React.useState<StateData<ProfileResult>>({} as StateData<ProfileResult>);
    const [otherProfileId, setOtherProfileId] = React.useState<string>("");

    // Get hash value from URL

    React.useEffect(() => {
        const hash = window.location.hash;
        if (hash) {
            const hashParts = hash.split("#");
            if (hashParts.length > 1) {
                const profileId = hashParts[1];
                setOtherProfileId(profileId);
                ApiComponentStateManager(ProfileServices.GetProfileById(profileId), setIsOtherProfile);
            }
        }
        else if (!profileState.data.id) {
            ApiGlobalStateManager(ProfileServices.GetProfile(), setProfileState);
        }
    }, []);

    return (
        <PageCard>
            {
                otherProfileId ?
                    (<ProfileDisplay {...isOtherProfile.data} />)
                    :
                    (
                        <>
                            {
                                profileState.data.id ?
                                    (<ProfileDisplay {...profileState.data} />)
                                    :
                                    (<Typography component="p">
                                        Loading...
                                    </Typography>)
                            }
                        </>
                    )
            }
        </PageCard>
    );
};
