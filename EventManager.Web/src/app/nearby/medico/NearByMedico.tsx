"use client";
import * as React from 'react';
import { Divider, IconButton, Stack, TextField as TextBox, Typography } from '@mui/material';
import { PageCard } from '@/components/PageCard';
import useStore from '@/state/GlobalState';
import { BusinessServices } from '@/services/ServicesIndex';
import { CallStatus } from '@/types/ApiTypes';
import { NearByBusinessDisplay } from '../business/NearByBusinessDisplay';
import SearchIcon from '@mui/icons-material/Search';
import { ApiGlobalStateManager } from '@/utils/ServiceStateHelper';

const txtStyle: React.CSSProperties = {
  width: 200,
}

const btnStyle: React.CSSProperties = {
  width: 50,
}

export const NearByMedico: React.FC = () => {
  const [pinCode, setPinCode] = React.useState<string>("");
  const [isformTouched, setIsFormTouched] = React.useState<boolean>(false);
  const { nearbyDoctorsListState, setNearbyDoctorsListState } = useStore();
  const doctorsCategory = "Medical and Healthcare";

  const isPinCodeValid = /^\d{6}$/.test(pinCode);
  const handleOnSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (!isPinCodeValid) {
      return;
    }
    ApiGlobalStateManager(BusinessServices.GetBusinessListByPinCodeCategory(Number(pinCode), doctorsCategory), setNearbyDoctorsListState);
  };

  return (
    <>
      <PageCard>
        <Typography variant="h6" component="h2" gutterBottom>
          Navodayan Medicos Nearby
        </Typography>
        <Typography variant="body2" color="textSecondary" gutterBottom>
          Find navodayan medical and healthcare professionals in your area using PIN code.
        </Typography>
        <Divider />
        <form onSubmit={handleOnSubmit}>
          <Stack direction='row' spacing={2} margin={2} height={50}>
            <TextBox
              variant="standard"
              style={txtStyle}
              label='Pin Code'
              type='text'
              value={pinCode}
              onChange={(e) => {
                setPinCode(e.target.value);
                setIsFormTouched(true);
              }}
              error={!isPinCodeValid && isformTouched}
              helperText={!isPinCodeValid && isformTouched ? "Pin Code must be 6 digits long" : ""}
            />
            <IconButton disabled={!isPinCodeValid} color="primary" aria-label="search" style={btnStyle} type='submit'>
              <SearchIcon />
            </IconButton>
          </Stack>
        </form>
      </PageCard>
      {
        (() => {
          switch (nearbyDoctorsListState.status) {
            case CallStatus.Success:
              {
                const businessList = nearbyDoctorsListState.data;
                if (businessList.length === 0) {
                  return <PageCard><Typography variant="body2" color="textSecondary">No businesses found in the area.</Typography></PageCard>;
                }
                return businessList.map((business, index) => (
                  <NearByBusinessDisplay key={index} {...business} />
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