"use client";
import React, { useState } from "react";
import { PostShimmer } from "../PostShimmer.";
import { PostDisplay } from "../PostDisplay";
import { PostResult } from "../../../types/PostApiTypes";
import { ApiResponse, CallStatus } from "../../../types/ApiTypes";
import { ISODateTimeToReadable } from "../../../utils/CommonHelper";
import { PostEmpty } from "../PostEmpty";
import useGlobalState, { StateDataPage } from "../../../state/GlobalState";
import { usePathname } from "next/navigation";
import { Pathname } from "@/constants/Routes";
import { PostServices } from "@/services/ServicesIndex";

export const NewsfeedPrivateView: React.FC = () => {
  const { privatePostListState: privatePostsState, setPrivatePostListState: setPrivatePostsState } = useGlobalState();
  const [offset, setOffset] = useState(0);
  const pathName = usePathname();

  React.useEffect(() => {
    if (!privatePostsState.isLastPage) {
      PostsManager(setPrivatePostsState, privatePostsState);
    }
  }, [])

  React.useEffect(() => {
    const handleScroll = () => {
      setOffset(window.scrollY);
      if ((window.innerHeight + window.scrollY + 1000) >= document.body.offsetHeight) {
        if (!privatePostsState.isLastPage) {
          PostsManager(setPrivatePostsState, privatePostsState);
        }
      }
    };

    if (pathName === Pathname.Newsfeed || pathName === Pathname.Root) {
      window.addEventListener('scroll', handleScroll);
    }

    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, [offset, pathName, privatePostsState, setPrivatePostsState]);

  return (
    <>
      {
        privatePostsState.status === CallStatus.Success ?
          (
            <>
              {
                privatePostsState.data.length > 0 ?
                  (
                    privatePostsState.data.map((post) => (
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
          ) : (<PostShimmer />)
      }
    </>
  );
};

const PostsManager = (
  setPrivatePosts: (state: StateDataPage<PostResult[]>) => void,
  privatePosts: StateDataPage<PostResult[]>
) => {
  PostServices.GetPosts(privatePosts.page + 1)
    .then((response: { data: ApiResponse<PostResult[]> }) => {
      const apiResult = response.data as ApiResponse<PostResult[]>;
      setPrivatePosts({
        data: [...privatePosts.data, ...apiResult.result],
        page: apiResult.result.length > 0 ? privatePosts.page + 1 : privatePosts.page,
        isLastPage: apiResult.result.length > 0 ? false : true,
        status: CallStatus.Success,
        timestamp: new Date(),
      });
    })
    .catch(() => {
      setPrivatePosts({
        data: [],
        page: privatePosts.page,
        isLastPage: false,
        status: CallStatus.Failure,
        timestamp: new Date(),
      });
    });
}