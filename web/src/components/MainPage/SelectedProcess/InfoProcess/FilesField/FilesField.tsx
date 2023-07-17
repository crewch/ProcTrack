import { Box, Tooltip, Typography } from '@mui/material'
import { CustomButton } from '../../../../CustomButton/CustomButton'
import { FC } from 'react'
import { useQuery } from '@tanstack/react-query'
import { passportApi } from '../../../../../api/passportApi'
import HistoryFilesDialog from './HistoryFilesDialog/HistoryFilesDialog'
import CommentFilesDialog from './CommentFilesDialog/CommentFilesDialog'
import { fileApi } from '../../../../../api/fileApi'
import { IUploadButtonProps } from '../../../../../interfaces/IMainPage/ISelectedProcess/IInfoProcess/IUploadButton/IUploadButton'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/FilesField.module.scss'

const FilesField: FC<IUploadButtonProps> = ({ processId }) => {
	const { data: passports, isSuccess } = useQuery({
		queryKey: ['passport', processId],
		queryFn: () => passportApi.getPassport(processId),
	})

	return (
		<Box className={styles.container}>
			{isSuccess && passports && passports[0]?.title ? (
				<Tooltip placement='top' title={passports[0].title} arrow>
					<CustomButton
						sx={{
							fontSize: {
								xs: '12px',
								lg: '14px',
							},
						}}
						variant='contained'
						startIcon={<img src='pdf-file.svg' height='20px' width='20px' />}
						onClick={() => fileApi.getFile(passports[0].title)}
					>
						{passports[0].title.slice(0, 10)}...
					</CustomButton>
				</Tooltip>
			) : (
				<CustomButton
					sx={{
						fontSize: {
							xs: '12px',
							lg: '14px',
						},
					}}
					disabled
					variant='contained'
					startIcon={<img src='pdf-file.svg' height='20px' width='20px' />}
				>
					Нет файла
				</CustomButton>
			)}
			{isSuccess && passports && passports[0]?.message && (
				<CommentFilesDialog message={passports[0].message} />
			)}
			{isSuccess && passports && (
				<HistoryFilesDialog passports={passports.slice(1)} />
			)}
			{isSuccess && passports && passports[0]?.createdAt && (
				<Typography className={styles.typography}>
					{passports[0].createdAt}
				</Typography>
			)}
		</Box>
	)
}

export default FilesField
