import { CallStatus } from "@/types/ApiTypes";
import { Alert, Backdrop, CircularProgress } from "@mui/material";

export interface IStatusMessageCase {
    status: CallStatus;
    message: string;
}

export interface IStatusMessageProps {
    style?: React.CSSProperties;
    notStartedMessage?: string;
    successMessage?: string;
    failureMessage?: string;
    currentStatus: CallStatus;
}

const rootStyle: React.CSSProperties = {
    marginTop: "1rem",
};

export const StatusMessage: React.FC<IStatusMessageProps> = (props) => {
    switch (props.currentStatus) {
        case CallStatus.NotStarted: {
            return props.notStartedMessage ? <Alert style={props.style ?? rootStyle} severity="info">{props.notStartedMessage}</Alert> : null;
        }
        case CallStatus.Success: {
            return props.successMessage ? <Alert style={props.style ?? rootStyle} severity="success">{props.successMessage}</Alert> : null;
        }
        case CallStatus.Failure: {
            return props.failureMessage ? <Alert style={props.style ?? rootStyle} severity="error">{props.failureMessage}</Alert> : null;
        }
        case CallStatus.InProgress: {
            return (
                <Backdrop sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }} open={props.currentStatus === CallStatus.InProgress} >
                    <CircularProgress color="inherit" />
                </Backdrop>
            );
        }
        default:
            return null;
    }
};