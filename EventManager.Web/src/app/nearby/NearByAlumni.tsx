"use client";
import * as React from 'react';
import { PageCard } from '../../components/PageCard';
import { PersonDisplay } from './PersonDisplay';
import { Divider, NativeSelect, Stack, Typography } from '@mui/material';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import { ProfileGeoUpdatePayload } from '../../types/ProfileApiTypes';
import { LoadingButton } from '@mui/lab';
import useGlobalState from '../../state/GlobalState';
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';
import { ProfileServices } from '@/services/ServicesIndex';
import { CallStatus } from '@/types/ApiTypes';

export const NearByAlumni: React.FC = () => {
  const { profileState, nearbyProfileListState, setProfileState, setNearbyProfileListState } = useGlobalState();
  const [range, setRange] = React.useState<number>(5);
  const [displayProgress, setDisplayProgress] = React.useState<boolean>(false);
  const [geoLocation, setGeoLocation] = React.useState<ProfileGeoUpdatePayload>({
    latitude: 0,
    longitude: 0
  });

  const updateGeoLocation = () => {
    setDisplayProgress(true);
    navigator.geolocation.getCurrentPosition(
      (position) => {
        const { latitude, longitude } = position.coords;
        setGeoLocation({ latitude, longitude });

        const lastUpdate = profileState.data.modifiedAt ? new Date(profileState.data.modifiedAt) : new Date(0);
        const currentTime = new Date();
        const diffMinutes = Math.ceil((currentTime.getTime() - lastUpdate.getTime()) / (1000 * 60));

        if (diffMinutes > 10 || profileState.data.latitude === 0 || profileState.data.longitude === 0) {
          ApiGlobalStateManager(ProfileServices.UpdateProfileGeo(latitude, longitude), setProfileState).then(() => {
            ApiGlobalStateManager(ProfileServices.GetProfilesNearby(range), setNearbyProfileListState);
            setDisplayProgress(false);
            return;
          });
        }
        else {
          ApiGlobalStateManager(ProfileServices.GetProfilesNearby(range), setNearbyProfileListState);
          setDisplayProgress(false);
          return;
        }
      },
      (error) => {
        console.error('Error fetching geolocation:', error);
        setDisplayProgress(false);
      }
    );
  };

  React.useEffect(() => {
    if (profileState.data.latitude !== 0 && profileState.data.longitude !== 0) {
      setGeoLocation({
        latitude: profileState.data.latitude,
        longitude: profileState.data.longitude
      });
    }
  }, [profileState.data.latitude, profileState.data.longitude]);

  return (
    <>
      <PageCard>
        <Typography variant="h6" component="h2" gutterBottom>
          Navodayans Nearby
        </Typography>
        <Typography variant="body2" color="textSecondary" gutterBottom>
          Turn ON your GPS and allow location access to view navodayans nearby.
        </Typography>
        <Divider />
        <Stack direction='column' component="div" spacing={2} margin={2}>
          <Typography variant="body2" color="textSecondary" component="div">
            Lat: {geoLocation?.latitude?.toFixed(5)}, Long: {geoLocation?.longitude?.toFixed(5)}
          </Typography>
          <Divider />
          <Stack direction='row' spacing={2} margin={2} maxWidth={240}>
            <NativeSelect
              id="range-select"
              value={range}
              required
              onChange={(e) => setRange(parseInt((e.target as HTMLSelectElement).value))}
            >
              <option value="1">1 km</option >
              <option value="2">2 km</option >
              <option value="3">3 km</option >
              <option value="4">4 km</option >
              <option value="5">5 km</option >
              <option value="10">10 km</option >
              <option value="15">15 km</option >
              <option value="20">20 km</option >
              <option value="25">25 km</option >
              <option value="30">30 km</option >
              <option value="35">35 km</option >
              <option value="40">40 km</option >
              <option value="45">45 km</option >
              <option value="50">50 km</option >
              <option value="500">500 km</option >
              <option value="5000">5000 km</option >
              <option value="10000">10000 km</option >
            </NativeSelect>
            <LoadingButton loadingPosition='start' loading={displayProgress} disabled={displayProgress} variant="contained" onClick={updateGeoLocation} startIcon={<LocationOnIcon />}>Update</LoadingButton>
          </Stack>
        </Stack>
      </PageCard>
      {
        (() => {
          switch (nearbyProfileListState.status) {
            case CallStatus.Success:
              {
                const profileList = nearbyProfileListState.data;
                if (profileList.length === 0) {
                  return <PageCard><Typography variant="body2" color="textSecondary">No businesses found in the area.</Typography></PageCard>;
                }
                return profileList.map((profile, index) => (
                  <PersonDisplay key={index} {...profile} />
                ));
              }
            case CallStatus.NotStarted:
              return <></>;
            case CallStatus.InProgress:
              return <PageCard><Typography variant="body2" color="textSecondary">Loading...</Typography></PageCard>;
            case CallStatus.Failure:
              return <PageCard><Typography variant="body2" color="textSecondary">Error loading data.</Typography></PageCard>;
            default:
              return null;
          }
        })()
      }
    </>
  );
}
