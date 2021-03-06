import React, { useEffect, useState } from "react";
import Video from './Video'
import { users } from "../modules/videoManager";
import { useParams } from "react-router-dom"
import { Link } from "react-router-dom";

const UserVideos = () => {
        const[videos, setVideos] = useState([]);
        const {id} = useParams();

        const getVideos = () => {
            users(id).then(videos => setVideos(videos));
        };

        useEffect(() => {
            getVideos();
        }, []);

  return (
    <>
        <div className="container">
            <div className="row justify-content-center">
                  {videos?.map((video) => (
                         <Video video= {video} key={video.id} />
                     ))}
            </div>
        </div>
    </>
  );
};
export default UserVideos;

