"use client";
import * as React from "react";
import { styled } from "@mui/material/styles";
import Card from "@mui/material/Card";
import CardHeader from "@mui/material/CardHeader";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Collapse from "@mui/material/Collapse";
import Avatar from "@mui/material/Avatar";
import IconButton, { IconButtonProps } from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import FavoriteIcon from "@mui/icons-material/Favorite";
import ShareIcon from "@mui/icons-material/Share";
import ExpandMoreIcon from "@mui/icons-material/ExpandMore";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import { Dialog } from "@mui/material";
import Image from "next/image";

interface ExpandMoreProps extends IconButtonProps {
  expand: boolean;
}

const ExpandMore = styled((props: ExpandMoreProps) => {
  const { ...other } = props;
  return <IconButton {...other} />;
})(({ theme, expand }) => ({
  transform: !expand ? "rotate(0deg)" : "rotate(180deg)",
  marginLeft: "auto",
  transition: theme.transitions.create("transform", {
    duration: theme.transitions.duration.shortest,
  }),
}));

export interface PostDisplayProps {
  author: string;
  authorImage: string;
  title: string;
  subheader: string;
  image: string;
  bodyPreview: string;
  bodyExpanded: string;
}

const titleStyle: React.CSSProperties = {
  fontSize: 16,
  color: "#666666",
  marginTop: -10,
  paddingBottom: 5,
}

export const PostDisplay: React.FunctionComponent<PostDisplayProps> = (props: PostDisplayProps) => {
  const [expanded, setExpanded] = React.useState(false);
  const [openDialog, setOpenDialog] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  const handleDialogClick = () => {
    setOpenDialog(!openDialog);
  }

  return (
    <>
      <Card sx={{ maxWidth: 760, mb: 1 }}>
        <CardHeader
          avatar={
            <Avatar aria-label="recipe" src={props.authorImage} alt={props.author}></Avatar>
          }
          action={
            <IconButton aria-label="settings">
              <MoreVertIcon />
            </IconButton>
          }
          title={<span style={titleStyle}>{props.author}</span>}
          subheader={props.subheader}
        />
        {props.image && <CardMedia onClick={handleDialogClick} component="img" height="194" image={props.image} alt={props.title} />}
        <CardContent>
          <Typography variant="subtitle2" style={titleStyle} color="text.secondary">
            {props.title}
          </Typography>
          <Typography variant="body2" color="text.secondary">
            {props.bodyPreview}
          </Typography>
        </CardContent>
        <CardActions disableSpacing>
          <IconButton aria-label="add to favorites">
            <FavoriteIcon />
          </IconButton>
          <IconButton aria-label="share">
            <ShareIcon />
          </IconButton>
          {props.bodyExpanded && (
            <>
              <ExpandMore expand={expanded} onClick={handleExpandClick} aria-expanded={expanded} aria-label="show more">
                <ExpandMoreIcon />
              </ExpandMore>
            </>
          )}
        </CardActions>
        {props.bodyExpanded && (
          <>
            <Collapse in={expanded} timeout="auto" unmountOnExit>
              <CardContent>
                <Typography paragraph>{props.bodyExpanded}</Typography>
              </CardContent>
            </Collapse>
          </>
        )}
      </Card>
      <Dialog
        open={openDialog}
        onClose={handleDialogClick}
        onClick={handleDialogClick}
      >
        <Image src={props.image} alt={props.title} />
      </Dialog>
    </>
  );
};