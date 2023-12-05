import { Box, Tooltip, Typography } from '@mui/material'
import { FC, memo } from 'react'
import { useQuery } from '@tanstack/react-query'
import HistoryFilesDialog from './HistoryFilesDialog/HistoryFilesDialog'
import CommentFilesDialog from './CommentFilesDialog/CommentFilesDialog'
import { GrayButton } from '@/components/ui/button/GrayButton'
import { fileService } from '@/services/file'
import { passportService } from '@/services/passport'
import styles from './FilesField.module.scss'

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
								xs: '0.75rem',
								lg: '0.875rem',
							},
						}}
						variant='contained'
						startIcon={
							<img src='pdf-file.svg' className={styles.grayButtonImg} />
						}
						onClick={() => fileService.getFile(passports[0].title)}
					>
						{passports[0].title.slice(0, 10)}...
					</GrayButton>
				</Tooltip>
			) : (
				<GrayButton
					sx={{
						fontSize: {
							xs: '0.75rem',
							lg: '0.875rem',
						},
					}}
					disabled
					variant='contained'
					startIcon={
						<img src='pdf-file.svg' className={styles.grayButtonImg} />
					}
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
