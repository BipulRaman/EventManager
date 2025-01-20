import { CallStatus } from "@/types/ApiTypes";
import { Alert, Backdrop, CircularProgress } from "@mui/material";

export interface IStatusMessageCase {
    status: CallStatus;
    message: string;
}

export interface IStatusMessageProps {
    style?: React.CSSProperties;
    display: boolean;
    notStartedMessage?: string;
    successMessage?: string;
    failureMessage?: string;
    currentStatus: CallStatus;
}

const rootStyle: React.CSSProperties = {
    marginTop: "1rem",
};

export const StatusMessage: React.FC<IStatusMessageProps> = (props) => {
    if (!props.display) {
        switch (props.currentStatus) {
            case CallStatus.NotStarted: {
                return props.notStartedMessage ? <div style={rootStyle}><Alert style={props.style} severity="info">{props.notStartedMessage}</Alert></div> : null;
            }
            case CallStatus.Success: {
                return props.successMessage ? <div style={rootStyle}><Alert style={props.style} severity="success">{props.successMessage}</Alert></div> : null;
            }
            case CallStatus.Failure: {
                return props.failureMessage ? <div style={rootStyle}><Alert style={props.style} severity="error">{props.failureMessage}</Alert></div> : null;
            }
            case CallStatus.InProgress: {
                return (
                    <Backdrop
                        sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                        open={props.currentStatus === CallStatus.InProgress}
                    >
                        <CircularProgress color="inherit" />
                    </Backdrop>
                );
            }
            default:
                return null;
        }
    }
    else {
        return null;
    }
};