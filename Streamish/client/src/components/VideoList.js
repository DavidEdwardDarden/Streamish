import React, { useEffect, useState } from "react";
import Video from './Video';
import { getAllVideos, searchVideos } from "../modules/videoManager";

export const VideoList = () => {
    //Every time state is updated, the component will re-render. 
  const [videos, setVideos] = useState([]);
  const [search, setSearch] = useState("");

  const getVideos = () => {
    if (search === "") {
    getAllVideos().then(videos => setVideos(videos));
    } else{
        searchVideos(search).then(videos => setVideos(videos));
    }
  };

  const handleSearch = (evt) => {
    evt.preventDefault()
    let searchInput = evt.target.value
    //set the state of setSearch
    setSearch(searchInput)
}

  useEffect(() => {
    getVideos();
  }, [search]);

  useEffect(() => {
    searchVideos();
}, [search]);


  return (
      <>
    <div>
    <input type='text' className="search" required onChange={handleSearch} id="search_box" placeholder="Search" />
</div>
    <div className="container">
      <div className="row justify-content-center">
        {videos.map((video) => (
          <Video video={video} key={video.id} />
        ))}
      </div>
    </div>
    </>
  );
};

export default VideoList;

//could have made this differently with the following code
//replacing lines 36-38
{/* <div className="container">
                <div className="row justify-content-center">
                    {videos.map((video) => (
                        <Video video={video} key={video.id} />
                    ))}
                </div>
            </div> */}