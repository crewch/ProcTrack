import { Box, Tooltip, Typography } from '@mui/material'
import { GrayButton } from '../../../../ui/button/GrayButton'
import { FC, memo } from 'react'
import { useQuery } from '@tanstack/react-query'
import HistoryFilesDialog from './HistoryFilesDialog/HistoryFilesDialog'
import CommentFilesDialog from './CommentFilesDialog/CommentFilesDialog'
import { passportService } from '../../../../../services/passport'
import { fileService } from '../../../../../services/file'
import styles from '/src/styles/MainPageStyles/SelectedProcessStyles/InfoProcessStyles/FilesFieldStyles/FilesField.module.scss'

interface FilesFieldProps {
	processId: number
}

const FilesField: FC<FilesFieldProps> = memo(({ processId }) => {
	const { data: passports, isSuccess } = useQuery({
		queryKey: ['passport', processId],
		queryFn: () => passportService.getPassport(processId),
	})

	return (
		<Box className={styles.container}>
			{isSuccess && passports && passports[0]?.title ? (
				<Tooltip placement='top' title={passports[0].title} arrow>
					<GrayButton
						sx={{
							fontSize: {
								xs: '12px',
								lg: '14px',
							},
						}}
						variant='contained'
						startIcon={<img src='pdf-file.svg' height='20px' width='20px' />}
						onClick={() => fileService.getFile(passports[0].title)}
					>
						{passports[0].title.slice(0, 10)}...
					</GrayButton>
				</Tooltip>
			) : (
				<GrayButton
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
				</GrayButton>
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
})

export default FilesField
