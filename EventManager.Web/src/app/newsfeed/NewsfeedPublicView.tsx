"use client";
import React, { useState } from "react";
import { PostShimmer } from "./PostShimmer.";
import { PostDisplay } from "./PostDisplay";
import { PostResult } from "../../types/PostApiTypes";
import { ApiResponse, CallStatus } from "../../types/ApiTypes";
import { ISODateTimeToReadable } from "../../utils/CommonHelper";
import { PostEmpty } from "./PostEmpty";
import useGlobalState, { StateDataPage } from "../../state/GlobalState";
import { usePathname } from "next/navigation";
import { Pathname } from "@/constants/Routes";
import { PostServices } from "@/services/ServicesIndex";

export const NewsfeedPublicView: React.FC = () => {
  const { publicPostListState: publicPostsState, setPublicPostListState: setPublicPostsState } = useGlobalState();
  const [offset, setOffset] = useState(0);
  const pathName = usePathname();

  React.useEffect(() => {
    if (!publicPostsState.isLastPage) {
      PostsManager(setPublicPostsState, publicPostsState);
    }
  }, [])

  React.useEffect(() => {
    const handleScroll = () => {
      setOffset(window.scrollY);
      if ((window.innerHeight + window.scrollY + 1000) >= document.body.offsetHeight) {
        if (!publicPostsState.isLastPage) {
          PostsManager(setPublicPostsState, publicPostsState);
        }
      }
    };

    if (pathName === Pathname.Newsfeed || pathName === Pathname.Root) {
      window.addEventListener('scroll', handleScroll);
    }

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, [offset, pathName, publicPostsState, setPublicPostsState]);

  return (
    <>
      {
        publicPostsState.status === CallStatus.Success ?
          (
            <>
              {
                publicPostsState.data.length > 0 ?
                  (
                    publicPostsState.data.map((post) => (
                      <PostDisplay
                        key={post.id}
                        author={post.createdByName}
                        authorImage={""}
                        title={post.title}
                        subheader={`🕒 ${ISODateTimeToReadable(post.createdAt)}`}
                        image={post.image}
                        bodyPreview={post.content}
                        bodyExpanded={""}
                      />))
                  ) : (<PostEmpty />)
              }
            </>
          ) : (
            <PostShimmer />
          )
      }
    </>
  );
};

const PostsManager = (
  setPublicPosts: (state: StateDataPage<PostResult[]>) => void,
  publicPosts: StateDataPage<PostResult[]>
) => {
  PostServices.GetPublicPosts(publicPosts.page + 1)
    .then((response: { data: ApiResponse<PostResult[]> }) => {
      const apiResult = response.data as ApiResponse<PostResult[]>;
      setPublicPosts({
        data: [...publicPosts.data, ...apiResult.result],
        page: apiResult.result.length > 0 ? publicPosts.page + 1 : publicPosts.page,
        isLastPage: apiResult.result.length > 0 ? false : true,
        status: CallStatus.Success,
        timestamp: new Date(),
      });
    })
    .catch(() => {
      setPublicPosts({
        data: [],
        page: publicPosts.page,
        isLastPage: false,
        status: CallStatus.Failure,
        timestamp: new Date(),
      });
    });
}

