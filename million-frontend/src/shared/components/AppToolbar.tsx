import React from 'react';
import { Toolbar } from 'primereact/toolbar';
import { Avatar } from 'primereact/avatar';
import { Badge } from 'primereact/badge';

const AppToolbar = () => {
    const startContent = (
        <React.Fragment>
            <img 
                className='w-34'
                src="https://cdn.millionluxury.com/image-resizing?image=https://maustorageprod.blob.core.windows.net/spinfileuat/Spin/Data/Estate/IMG/ceb693ad6b7643fc8c1be271d6a9c068.svg?v=740" 
                alt="million-logo" 
            />
        </React.Fragment>
    )

    const endContent = (
        <a href="https://www.linkedin.com/in/bdmtnz/" target="_blank" rel="noopener noreferrer">
            <div className='flex gap-4 items-center'>
                Brayan Martinez
                <Avatar 
                    className="p-overlay-badge" 
                    image="https://media.licdn.com/dms/image/v2/D5603AQEGiNMCx5slNg/profile-displayphoto-shrink_200_200/B56ZVSocG_GoAY-/0/1740848091279?e=1758153600&v=beta&t=5OgBDTxdq1xw7goO_FhznVP8uAeQlRddfXbB7I_PvVs"
                    shape='circle'
                    size="xlarge">
                    <Badge value="1" severity="danger"/>
                </Avatar>
            </div>
        </a>
    )

    return (
        <div className="card bg-[#1f2937] flex justify-center">
            <div className="w-full max-w-7xl">
                <Toolbar start={startContent} end={endContent} />
            </div>
        </div>
    )
}

export default AppToolbar